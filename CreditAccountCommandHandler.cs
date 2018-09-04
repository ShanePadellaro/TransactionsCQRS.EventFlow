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
            var result = aggregate.Credit(command.Amount);

            return Task.FromResult<IExecutionResult>(new SuccessExecutionResult());
        }
    }
}