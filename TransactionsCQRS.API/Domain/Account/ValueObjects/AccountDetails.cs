using EventFlow.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ValueObjects
{
    public class AccountDetails:ValueObject
    {
        public string Externalid { get; }
        public string Name { get; }
        public string CountryCode { get; }
        public string CurrencyCode { get; }
        public int StartingBalance { get; }

        public AccountDetails(string externalid, string name, string countryCode, string currencyCode,
            int startingBalance)
        {
            Externalid = externalid;
            Name = name;
            CountryCode = countryCode;
            CurrencyCode = currencyCode;
            StartingBalance = startingBalance;
        }
    }
}