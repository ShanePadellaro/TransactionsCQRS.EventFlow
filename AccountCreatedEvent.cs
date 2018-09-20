using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
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