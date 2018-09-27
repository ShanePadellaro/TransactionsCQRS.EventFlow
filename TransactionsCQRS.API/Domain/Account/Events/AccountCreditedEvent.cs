using EventFlow.Aggregates;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.Events
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