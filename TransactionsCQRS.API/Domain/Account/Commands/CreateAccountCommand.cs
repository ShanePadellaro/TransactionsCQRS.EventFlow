using EventFlow.Commands;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.Commands
{
    public class CreateAccountCommand:Command<AccountAggregate,AccountId,AccountOpenedRecipt>
    {
        public AccountDetails AccountDetails { get; }

        public CreateAccountCommand(AccountDetails accountDetails) : base(AccountId.New)
        {
            AccountDetails = accountDetails;
        }
    }
}