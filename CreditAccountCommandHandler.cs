using System.Threading;
using System.Threading.Tasks;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow
{
    public class
        CreditAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, CreditAccountCommand
        >
    {
        public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate,
            CreditAccountCommand command, CancellationToken cancellationToken)
        {
            var result = aggregate.Credit(command.Transaction);
            return Task.FromResult<IExecutionResult>(result);
        }
        
    }
}