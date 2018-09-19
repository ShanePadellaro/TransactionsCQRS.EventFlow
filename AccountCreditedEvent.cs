using System;
using System.Collections.Generic;
using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class AccountCreditedEvent:AggregateEvent<AccountAggregate, AccountId>
    {
        public Transaction Transaction { get; }
        public long CurrentAccountBalance { get; }
        public long NewAccountBalance { get; }

        public AccountCreditedEvent(Transaction transaction, long currentAccountBalance, long newAccountBalance)
        {
            Transaction = transaction;
            CurrentAccountBalance = currentAccountBalance;
            NewAccountBalance = newAccountBalance;
        }
    }
}