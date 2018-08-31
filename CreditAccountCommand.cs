using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow
{
    public class CreditAccountCommand:Command<AccountAggregate,AccountId,IExecutionResult>
    {
        public long Amount { get; }

        public CreditAccountCommand(AccountId aggregateId,long amount) : base(aggregateId)
        {
            Amount = amount;
        }
    }
}