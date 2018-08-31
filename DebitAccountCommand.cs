using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow
{
    public class DebitAccountCommand : Command<AccountAggregate, AccountId, IExecutionResult>
    {
        public long Amount { get; }

        public DebitAccountCommand(AccountId aggregateId, long amount) : base(aggregateId)
        {
            Amount = amount;
        }
    }
}