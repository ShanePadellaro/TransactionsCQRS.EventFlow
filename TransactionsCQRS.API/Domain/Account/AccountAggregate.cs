using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;
using TransactionsCQRS.API.Domain.Account.Events;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account
{
    public class AccountAggregate : SnapshotAggregateRoot<AccountAggregate, AccountId, AccountSnapshot>
    {
        public long Balance { get; private set; }
        public string CountryCode { get; private set; }
        public string CurrencyCode { get; private set; }
        public string ExternalId { get; private set; }
        public string AccountName { get; set; }

        public AccountAggregate(AccountId id) : base(id, SnapshotEveryFewVersionsStrategy.With(5))
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
            AccountName = @event.AccountDetails.Name;
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
            var snapshot = new AccountSnapshot()
            {
                Balance = this.Balance,
                CountryCode = this.CountryCode,
                CurrencyCode = this.CurrencyCode,
                AccountName = this.AccountName,
                ExternalId = this.ExternalId
            };
            return Task.FromResult(snapshot);
        }

        protected override Task LoadSnapshotAsync(AccountSnapshot snapshot, ISnapshotMetadata metadata,
            CancellationToken cancellationToken)
        {
            Balance = snapshot.Balance;
            CountryCode = snapshot.CountryCode;
            CurrencyCode = snapshot.CountryCode;
            AccountName = snapshot.AccountName;
            ExternalId = snapshot.ExternalId;

            return Task.CompletedTask;
        }

        public AccountOpenedRecipt OpenAccount(AccountDetails accountDetails)
        {
//            if(Version > 0)
//                return new FailedExecutionResult("AggregateVersion can't be more than ");
            var recipt = new AccountOpenedRecipt(accountDetails,Id);
            Emit(new AccountCreatedEvent(accountDetails));
            return recipt;
        }
    }
}