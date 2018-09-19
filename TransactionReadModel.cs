using System;
using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    [MongoDbCollectionName("TransactionReadModel")]
    public class TransactionReadModel : IMongoDbReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>
    {
        
        
        public Transaction Transaction { get; set; }
        public string _id { get; set; }
        public long? _version { get; set; }
        public long PreviousBalance { get; set; }
            
            
        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent> domainEvent)
        {
            PreviousBalance = domainEvent.AggregateEvent.CurrentBalance;
            _id = context.ReadModelId;
        }


        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
            _id = context.ReadModelId;
            PreviousBalance = domainEvent.AggregateEvent.CurrentAccountBalance;
            Transaction = domainEvent.AggregateEvent.Transaction;

        }

    }
}
    