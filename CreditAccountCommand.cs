using System;
using System.Collections.Generic;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace TransactionsCQRS.EventFlow
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