using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using TransactionsCQRS.EventFlow.Domain.Account.Events;
using TransactionsCQRS.EventFlow.Domain.Account.ValueObjects;

namespace TransactionsCQRS.EventFlow.Domain.Account.ReadModels
{
    public class TransactionReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> || domainEvent is IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent>)
            {
                yield return TransactionId.New.Value;
            }
        }
    }
}