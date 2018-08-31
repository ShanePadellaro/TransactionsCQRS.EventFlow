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

        public IExecutionResult Credit(long amount)
        {
            Emit(new AccountCreditedEvent(amount));
            return new SuccessExecutionResult();
        }
        
        public void Apply(AccountCreditedEvent @event)
        {
            Balance = +@event.Amount;
        }
        
        public void Apply(AccountDebitedEvent @event)
        {
            Balance = -@event.Amount;
        }

        public IExecutionResult Debit(long amount)
        {
            Emit(new AccountDebitedEvent(amount));
            return new SuccessExecutionResult();
        }
    }

    public class AccountDebitedEvent:AggregateEvent<AccountAggregate,AccountId>
    {
        public long Amount { get; }

        public AccountDebitedEvent(long amount)
        {
            Amount = amount;
        }
    }
}