using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    [MongoDbCollectionName("AccountReadModel")]
    public class AccountReadModel : IMongoDbReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountBalanceChangedEvent>
    {

        public long Balance { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
            _id = domainEvent.AggregateIdentity.Value;
            Balance = domainEvent.AggregateEvent.NewAccountBalance;
        }


        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent> domainEvent)
        {
            _id = domainEvent.AggregateIdentity.Value;
            Balance = domainEvent.AggregateEvent.NewAccountBalance;
        }

        public string _id { get; set; }
        public long? _version { get; set; }
        
        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountBalanceChangedEvent> domainEvent)
        {
            _id = domainEvent.AggregateIdentity.Value;
            Balance = domainEvent.AggregateEvent.NewBalance;
        }
    }
}