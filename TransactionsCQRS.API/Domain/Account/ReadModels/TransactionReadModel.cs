using System;
using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;
using TransactionsCQRS.API.Domain.Account.Events;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Domain.Account.ReadModels
{
    [MongoDbCollectionName("TransactionReadModel")]
    public class TransactionReadModel : IMongoDbReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>
    {
        
        
        public string _id { get; set; }
        public long? _version { get; set; }
        public long PreviousBalance { get; set; }
        
        public string ExternalId { get; private set; }
        public string AccountId { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public List<Dictionary<string, object>> Properties { get; set; }
        public long Amount { get; private set; }
        public long Tax { get; private set; }
        
        public DateTime BillingDate { get; set; }
        public long Taxrate { get; private set; }
        public string CountryCode { get; private set; }
        public string CurrencyCode { get; private set; }
        public List<TransactionItem> TransactionItems { get; private set; }
            
            
        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent> domainEvent)
        {
            PreviousBalance = domainEvent.AggregateEvent.CurrentBalance;
            _id = context.ReadModelId;
            
            Map(domainEvent.AggregateEvent.Transaction);
        }


        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
            _id = context.ReadModelId;
            PreviousBalance = domainEvent.AggregateEvent.CurrentAccountBalance;
            
            Map(domainEvent.AggregateEvent.Transaction);
        }

        private void Map(Transaction transaction)
        {
            ExternalId = transaction.ExternalId;
            AccountId = transaction.AccountId;
            Description = transaction.Description;
            Type = transaction.Type;
            Properties = transaction.Properties;
            Amount = transaction.Amount;
            Tax = transaction.Tax;
            BillingDate = transaction.BillingDate;
            Taxrate = transaction.Taxrate;
            CountryCode = transaction.CountryCode;
            CurrencyCode = transaction.CountryCode;
            TransactionItems = transaction.TransactionItems;
        }
    }
}
    