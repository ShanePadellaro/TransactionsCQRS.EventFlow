using System.Collections.Generic;
using EventFlow.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ValueObjects
{
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
}