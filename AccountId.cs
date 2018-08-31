using EventFlow.Core;

namespace TransactionsCQRS.EventFlow
{
    public class AccountId :
        Identity<AccountId>
    {
        public AccountId(string value) : base(value)
        {
        }
    }
}