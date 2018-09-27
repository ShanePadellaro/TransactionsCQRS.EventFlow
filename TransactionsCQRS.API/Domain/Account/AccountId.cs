using EventFlow.Core;

namespace TransactionsCQRS.API.Domain.Account
{
    public class AccountId :
        Identity<AccountId>
    {
        public AccountId(string value) : base(value)
        {
        }
    }
}