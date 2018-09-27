using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using TransactionsCQRS.API.Domain.Account.Events;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ReadModels
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