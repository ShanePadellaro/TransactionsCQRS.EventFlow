namespace TransactionsCQRS.EventFlow
{
    public class TransactionReadModel
    {
        public long Amount { get; }
        public TransactionType Type { get; }
        public long PreviousBalance { get; }

        public TransactionReadModel(long amount, TransactionType type, long previousBalance)
        {
            Amount = amount;
            Type = type;
            PreviousBalance = previousBalance;
        }
    }
}