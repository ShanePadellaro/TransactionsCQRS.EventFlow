using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow
{
    public class
        DebitAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, DebitAccountCommand>
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate,
            DebitAccountCommand command, CancellationToken cancellationToken)
        {
            var result = aggregate.Debit(command.Amount);
            return Task.FromResult<IExecutionResult>(new SuccessExecutionResult());
        }
    }
}