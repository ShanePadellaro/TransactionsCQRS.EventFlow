using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class Transaction : AggregateRoot<Transaction, TransactionId>
    {
        public AccountId AccountId { get; }
        public string Type { get; set; }
        public long Balance { get; private set; }
        public long Amount { get; set; }

        public Transaction(AccountId accountId,TransactionId id,string type,long balance,long amount) : base(id)
        {
            AccountId = accountId;
            Type = type;
            Balance = balance;
            Amount = amount;
        }

        public void Test()
        {
            Emit(new TransactionCreatedEvent(AccountId,Type,Amount,Balance));
        }
        public void Apply(TransactionCreatedEvent @event)
        {
            Amount = @event.Amount;
        }
    }
}