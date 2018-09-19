using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow
{
    public class DebitAccountCommand : Command<AccountAggregate, AccountId, IExecutionResult>
    {
        public Transaction Transaction { get; }

        public DebitAccountCommand(AccountId aggregateId, Transaction transaction) : base(aggregateId)
        {
            Transaction = transaction;
        }
    }
}