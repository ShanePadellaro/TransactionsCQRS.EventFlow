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

        public Transaction Credit(long amount)
        {
            Emit(new AccountCreditedEvent(amount,Balance));
            return new Transaction(Id,TransactionId.New,"credit",Balance,amount);;

        }
        
        public void Apply(AccountCreditedEvent @event)
        {
            Balance += @event.Amount;
        }
        
        public void Apply(AccountDebitedEvent @event)
        {
            Balance -= @event.Amount;
        }

        public Transaction Debit(long amount)
        {
            Emit(new AccountDebitedEvent(amount,Balance));
            var x = new Transaction(Id,TransactionId.New,"debit",Balance,amount);
            
            x.Test();
            return x;
        }
    }
}