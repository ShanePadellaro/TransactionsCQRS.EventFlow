using EventFlow.Snapshots;

namespace TransactionsCQRS.API.Domain.Account
{
    [SnapshotVersion("account", 1)]
    public class AccountSnapshot : ISnapshot
    {
        public long Balance { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string AccountName { get; set; }
        public string ExternalId { get; set; }
    }
}