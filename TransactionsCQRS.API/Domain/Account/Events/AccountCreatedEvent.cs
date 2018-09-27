using EventFlow.Aggregates;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.Events
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