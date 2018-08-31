using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class AccountCreditedEvent:AggregateEvent<AccountAggregate, AccountId>
    {
        public long Amount { get; }

        public AccountCreditedEvent(long amount)
        {
            Amount = amount;
        }
    }

    
}