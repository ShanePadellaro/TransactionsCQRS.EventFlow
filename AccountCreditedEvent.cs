﻿using EventFlow.Aggregates;

namespace TransactionsCQRS.EventFlow
{
    public class AccountCreditedEvent:AggregateEvent<AccountAggregate, AccountId>
    {
        public long Amount { get; }
        public long Balance { get; }

        public AccountCreditedEvent(long amount, long balance)
        {
            Amount = amount;
            Balance = balance;
        }
    }
}