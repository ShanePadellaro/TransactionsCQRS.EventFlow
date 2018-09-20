using EventFlow.Core;

namespace TransactionsCQRS.EventFlow.Domain.Account.ValueObjects
{
    public class TransactionId : Identity<TransactionId>
    {
        public TransactionId(string value) : base(value)
        {
        }
    }
}