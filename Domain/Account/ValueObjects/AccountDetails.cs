using EventFlow.ValueObjects;

namespace TransactionsCQRS.EventFlow.Domain.Account.ValueObjects
{
    public class AccountDetails:ValueObject
    {
        public string Externalid { get; }
        public string CountryCode { get; }
        public string CurrencyCode { get; }
        public int StartingBalance { get; }

        public AccountDetails(string externalid, string countryCode, string currencyCode, int startingBalance)
        {
            Externalid = externalid;
            CountryCode = countryCode;
            CurrencyCode = currencyCode;
            StartingBalance = startingBalance;
        }
    }
}