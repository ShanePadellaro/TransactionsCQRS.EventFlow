using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.API.Domain.Account.Commands
{
    public class
        DebitAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, DebitAccountCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate,
            DebitAccountCommand command, CancellationToken cancellationToken)
        {
            var result = aggregate.Debit(command.Transaction);
            return Task.FromResult<IExecutionResult>(new SuccessExecutionResult());
        }
    }
}