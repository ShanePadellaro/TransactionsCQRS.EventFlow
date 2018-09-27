using EventFlow.Core;

namespace TransactionsCQRS.API.Domain.Account.ValueObjects
{
    public class TransactionId : Identity<TransactionId>
    {
        public TransactionId(string value) : base(value)
        {
        }
    }
}