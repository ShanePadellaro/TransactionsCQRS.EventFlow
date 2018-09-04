using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace TransactionsCQRS.EventFlow
{
    public class TransactionReadModelLocator : IReadModelLocator
    {
        public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
        {
            if (domainEvent is IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> || domainEvent is IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent>)
            {
                yield return TransactionId.New.Value;
            }

            yield break;
        }
    }
}