using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class TransactionCreatedEvent:AggregateEvent<Transaction,TransactionId>
    {
        public AccountId AccountId { get; }
        public string Type { get; }
        public long Amount { get; }
        public long Balance { get; }

        public TransactionCreatedEvent(AccountId accountId, string type, long amount, long balance)
        {
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Balance = balance;
        }
    }
}