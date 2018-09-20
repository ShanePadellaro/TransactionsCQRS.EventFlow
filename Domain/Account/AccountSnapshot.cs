using EventFlow.Snapshots;

namespace TransactionsCQRS.EventFlow.Domain.Account
{
    [SnapshotVersion("account", 1)]
    public class AccountSnapshot : ISnapshot
    {
        public long Balance { get; set; }
    }
}