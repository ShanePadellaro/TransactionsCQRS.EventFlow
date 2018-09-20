using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;
using TransactionsCQRS.EventFlow.Domain.Account.Events;

namespace TransactionsCQRS.EventFlow.Domain.Account.ReadModels
{
    [MongoDbCollectionName("AccountReadModel")]
    public class AccountReadModel : IMongoDbReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountCreatedEvent>
    {
        public string CurrencyCode { get; set; }
        public string CountryCode { get; set; }
        public long Balance { get; set; }
        public string ExternalId { get; set; }


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

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountCreatedEvent> domainEvent)
        {
            _id = domainEvent.AggregateIdentity.Value;
            Balance = domainEvent.AggregateEvent.AccountDetails.StartingBalance;
            ExternalId = domainEvent.AggregateEvent.AccountDetails.Externalid;
            CountryCode = domainEvent.AggregateEvent.AccountDetails.CountryCode;
            CurrencyCode = domainEvent.AggregateEvent.AccountDetails.CurrencyCode;
            AccountName = domainEvent.AggregateEvent.AccountDetails.Name;
        }

        public string AccountName { get; set; }
    }
}