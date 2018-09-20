using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class AccountBalanceChangedEvent : IAggregateEvent<AccountAggregate, AccountId>
    {
        public long NewBalance { get; }
        public long OldBalance { get; }

        public AccountBalanceChangedEvent(long newBalance, long oldBalance)
        {
            NewBalance = newBalance;
            OldBalance = oldBalance;
        }
    }
}