using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow.Domain.Account.Commands
{
    public class CreateAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, CreateAccountCommand>
    {
        public override Task ExecuteAsync(AccountAggregate aggregate, CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var result= aggregate.OpenAccount(command.AccountDetails);
            return Task.FromResult(result);
        }
    }
}