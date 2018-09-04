using EventFlow.Core;

namespace TransactionsCQRS.EventFlow
{
    public class TransactionId : Identity<TransactionId>
    {
        public TransactionId(string value) : base(value)
        {
        }
    }
}