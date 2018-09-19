using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;

namespace TransactionsCQRS.EventFlow
{
    public class AccountAggregate : SnapshotAggregateRoot<AccountAggregate, AccountId,AccountSnapshot>
    {
        public long Balance { get; private set; }
        public AccountAggregate(AccountId id) : base(id,SnapshotEveryFewVersionsStrategy.With(100))
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

        public IExecutionResult Credit(Transaction transaction)
        {
            var newBalance = Balance + transaction.Amount;
            Emit(new AccountCreditedEvent(transaction,Balance,newBalance));
            return new SuccessExecutionResult();

        }

        public IExecutionResult Debit(Transaction transaction)
        {
            var newBalance = Balance - transaction.Amount;
            Emit(new AccountDebitedEvent(transaction,Balance,newBalance));
            return new SuccessExecutionResult();
        }

        protected override Task<AccountSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            var snapshot = new AccountSnapshot() {Balance = this.Balance};
            return Task.FromResult(snapshot);
        }

        protected override Task LoadSnapshotAsync(AccountSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
        {
            Balance = snapshot.Balance;

            return Task.CompletedTask;
        }
    }

    public class AccountBalanceChangedEvent:IAggregateEvent<AccountAggregate,AccountId>
    {
        public long NewBalance { get; }
        public long OldBalance { get; }

        public AccountBalanceChangedEvent(long newBalance, long oldBalance)
        {
            NewBalance = newBalance;
            OldBalance = oldBalance;
        }
    }

    [SnapshotVersion("account", 1)]
    public class AccountSnapshot : ISnapshot
    {
        public long Balance { get; set; }
    }

    
}