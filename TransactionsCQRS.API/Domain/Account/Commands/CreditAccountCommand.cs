using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.Commands
{
    public class CreditAccountCommand:Command<AccountAggregate,AccountId,IExecutionResult>
    {
        public Transaction Transaction { get; }


        public CreditAccountCommand(AccountId aggregateId, Transaction transaction ) : base(aggregateId)
        {
            Transaction = transaction;
        }
    }
}