using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class AccountDebitedEvent:AggregateEvent<AccountAggregate,AccountId>
    {
        public long Amount { get; }
        public long Balance { get; }

        public AccountDebitedEvent(long amount, long balance)
        {
            Amount = amount;
            Balance = balance;
        }
    }
}