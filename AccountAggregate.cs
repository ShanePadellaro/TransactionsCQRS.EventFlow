using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;

namespace TransactionsCQRS.EventFlow
{
    public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
    {
        public long Balance { get; private set; }
        public AccountAggregate(AccountId id) : base(id)
        {
        }


        private void ChangeBalance(long transactionAmount)
        {
            Balance = transactionAmount;
        }

        public void Apply(AccountDebitedEvent @event)
        {   
            ChangeBalance(@event.Transaction.Amount);
        }
        
        
        public void Apply(AccountCreditedEvent @event)
        {
            ChangeBalance(@event.Transaction.Amount);
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
}