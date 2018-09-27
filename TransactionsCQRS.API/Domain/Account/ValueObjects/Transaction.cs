using System;
using System.Collections.Generic;
using EventFlow.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ValueObjects
{
    public class Transaction : ValueObject
    {
        public Transaction(string externalId, string accountId, string description, string type, long amount, long tax,
            DateTime billingDate, long taxrate, string countryCode, string currencyCode,
            List<TransactionItem> transactionItems, List<Dictionary<string, object>> properties = null)
        {
            ExternalId = externalId;
            AccountId = accountId;
            Description = description;
            Type = type;
            Properties = properties;
            Amount = amount;
            Tax = tax;
            BillingDate = billingDate;
            Taxrate = taxrate;
            CountryCode = countryCode;
            CurrencyCode = currencyCode;
            TransactionItems = transactionItems;
        }

        public string ExternalId { get; private set; }
        public string AccountId { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public List<Dictionary<string, object>> Properties { get; }
        public long Amount { get; private set; }
        public long Tax { get; private set; }
        public DateTime BillingDate { get; }
        public long Taxrate { get; private set; }
        public string CountryCode { get; private set; }
        public string CurrencyCode { get; private set; }
        public List<TransactionItem> TransactionItems { get; private set; }
    }
}