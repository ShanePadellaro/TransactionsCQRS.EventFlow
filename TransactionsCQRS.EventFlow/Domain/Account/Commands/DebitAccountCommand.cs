using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using TransactionsCQRS.EventFlow.Domain.Account.ValueObjects;

namespace TransactionsCQRS.EventFlow.Domain.Account.Commands
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