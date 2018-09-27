using EventFlow.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ValueObjects
{
    public class Fee : ValueObject
    {
        public Fee(string companyId, string label, string originalCurrency, string conversionRate, long tax,
            long taxRate)
        {
            CompanyId = companyId;
            Label = label;
            OriginalCurrency = originalCurrency;
            ConversionRate = conversionRate;
            Tax = tax;
            TaxRate = taxRate;
        }

        public string CompanyId { get; private set; }
        public string Label { get; private set; }
        public string OriginalCurrency { get; private set; }
        public string ConversionRate { get; private set; }
        public long Tax { get; private set; }
        public long TaxRate { get; private set; }
    }
}