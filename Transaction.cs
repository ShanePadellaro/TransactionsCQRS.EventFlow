using System;
using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ValueObjects;

namespace TransactionsCQRS.EventFlow
{
    public class Transaction : ValueObject
    {
        public Transaction(string externalId, Guid accountId, string description, string type, long amount, long tax,
            DateTimeOffset billingDate, long taxrate, string countryCode, string currencyCode,
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
        public Guid AccountId { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public List<Dictionary<string, object>> Properties { get; }
        public long Amount { get; private set; }
        public long Tax { get; private set; }
        public DateTimeOffset BillingDate { get; }
        public long Taxrate { get; private set; }
        public string CountryCode { get; private set; }
        public string CurrencyCode { get; private set; }
        public List<TransactionItem> TransactionItems { get; private set; }
    }

    public class TransactionItem : ValueObject
    {
        public TransactionItem(long amount, string type, long units, List<KeyValuePair> keyValuePairs = null, List<Fee> subFees = null)
        {
            Amount = amount;
            Type = type;
            Units = units;
            KeyValuePairs = keyValuePairs;
            SubFees = subFees;
        }

        public long Amount { get; private set; }
        public string Type { get; private set; }
        public long Units { get; private set; }
        public List<Fee> SubFees { get; private set; }
        public List<KeyValuePair> KeyValuePairs { get; private set; }

    }

    

    public class Fee : ValueObject
    {
        public Fee(Guid companyId, string label, string originalCurrency, string conversionRate, long tax,
            long taxRate)
        {
            CompanyId = companyId;
            Label = label;
            OriginalCurrency = originalCurrency;
            ConversionRate = conversionRate;
            Tax = tax;
            TaxRate = taxRate;
        }

        public Guid CompanyId { get; private set; }
        public string Label { get; private set; }
        public string OriginalCurrency { get; private set; }
        public string ConversionRate { get; private set; }
        public long Tax { get; private set; }
        public long TaxRate { get; private set; }
    }
    

    public class KeyValuePair : ValueObject
    {
        public KeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}