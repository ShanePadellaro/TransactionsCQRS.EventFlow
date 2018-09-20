using EventFlow.Aggregates;
using TransactionsCQRS.EventFlow.Domain.Account.ValueObjects;

namespace TransactionsCQRS.EventFlow.Domain.Account.Events
{
    public class AccountCreatedEvent : IAggregateEvent<AccountAggregate, AccountId>
    {
        public AccountDetails AccountDetails { get; }

        public AccountCreatedEvent(AccountDetails accountDetails)
        {
            AccountDetails = accountDetails;
        }
    }
}