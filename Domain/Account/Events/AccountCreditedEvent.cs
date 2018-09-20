using EventFlow.Aggregates;
using TransactionsCQRS.EventFlow.Domain.Account.ValueObjects;

namespace TransactionsCQRS.EventFlow.Domain.Account.Events
{
    public class AccountCreditedEvent:AggregateEvent<AccountAggregate, AccountId>
    {
        public Transaction Transaction { get; }
        public long CurrentAccountBalance { get; }
        public long NewAccountBalance { get; }

        public AccountCreditedEvent(Transaction transaction, long currentAccountBalance, long newAccountBalance)
        {
            Transaction = transaction;
            CurrentAccountBalance = currentAccountBalance;
            NewAccountBalance = newAccountBalance;
        }
    }
}