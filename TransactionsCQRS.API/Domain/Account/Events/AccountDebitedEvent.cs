using EventFlow.Aggregates;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.Events
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