using EventFlow.Core;

namespace TransactionsCQRS.EventFlow.Domain.Account
{
    public class AccountId :
        Identity<AccountId>
    {
        public AccountId(string value) : base(value)
        {
        }
    }
}