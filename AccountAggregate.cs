using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;
using MongoDB.Driver;

namespace TransactionsCQRS.EventFlow
{
    public class AccountAggregate : SnapshotAggregateRoot<AccountAggregate, AccountId, AccountSnapshot>
    {
        public long Balance { get; private set; }
        public string CountryCode { get; private set; }
        public string CurrencyCode { get; private set; }
        public string ExternalId { get; private set; }

        public AccountAggregate(AccountId id) : base(id, SnapshotEveryFewVersionsStrategy.With(100))
        {
        }


        private void ChangeBalance(long transactionAmount)
        {
            Balance = transactionAmount;
        }

        public void Apply(AccountDebitedEvent @event)
        {
            ChangeBalance(@event.NewAccountBalance);
        }


        public void Apply(AccountCreditedEvent @event)
        {
            ChangeBalance(@event.NewAccountBalance);
        }

        public void Apply(AccountCreatedEvent @event)
        {
            ExternalId = @event.AccountDetails.Externalid;
            Balance = @event.AccountDetails.StartingBalance;
            CountryCode = @event.AccountDetails.CountryCode;
            CurrencyCode = @event.AccountDetails.CurrencyCode;
        }


        public IExecutionResult Credit(Transaction transaction)
        {
            var newBalance = Balance + transaction.Amount;
            Emit(new AccountCreditedEvent(transaction, Balance, newBalance));
            return new SuccessExecutionResult();
        }

        public IExecutionResult Debit(Transaction transaction)
        {
            var newBalance = Balance - transaction.Amount;
            Emit(new AccountDebitedEvent(transaction, Balance, newBalance));
            return new SuccessExecutionResult();
        }

        protected override Task<AccountSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            var snapshot = new AccountSnapshot() {Balance = this.Balance};
            return Task.FromResult(snapshot);
        }

        protected override Task LoadSnapshotAsync(AccountSnapshot snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            Balance = snapshot.Balance;

            return Task.CompletedTask;
        }

        public IExecutionResult OpenAccount(AccountDetails commandAccountDetails)
        {
//            if(Version > 0)
//                return new FailedExecutionResult("AggregateVersion can't be more than ");
            Emit(new AccountCreatedEvent(commandAccountDetails));
            return new SuccessExecutionResult();
        }
    }
}