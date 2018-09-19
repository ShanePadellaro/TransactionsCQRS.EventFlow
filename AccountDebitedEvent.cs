using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class AccountDebitedEvent:AggregateEvent<AccountAggregate,AccountId>
    {
        public Transaction Transaction { get; }
        public long CurrentBalance { get; }
        public long NewAccountBalance { get; }

        public AccountDebitedEvent(Transaction transaction, long currentBalance, long newAccountBalance)
        {
            Transaction = transaction;
            CurrentBalance = currentBalance;
            NewAccountBalance = newAccountBalance;
        }
    }
}