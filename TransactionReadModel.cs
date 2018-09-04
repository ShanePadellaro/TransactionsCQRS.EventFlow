using System;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    [ElasticsearchType(IdProperty = "Id", Name = "transaction")]
    public class TransactionReadModel : IReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>
    {
        [Number(NumberType.Integer, Name = "Amount", Index = false)]
        public long Amount { get; set; }

        [Nest.Text(Name = "Type", Index = false)]
        public string Type { get; set; }
        
        [Nest.Text(Name = "AccountId", Index = false)]
        public string AccountId { get; set; }

        [Number(NumberType.Integer, Name = "PreviousBalance", Index = false)]
        public long PreviousBalance { get; set; }

        [Keyword(Index = true)] public string Id { get; set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Amount = domainEvent.AggregateEvent.Amount;
            Type = "Debit";
            PreviousBalance = domainEvent.AggregateEvent.Balance;
        }


        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Amount = domainEvent.AggregateEvent.Amount;
            Type = "Credit";
            PreviousBalance = domainEvent.AggregateEvent.Balance;
        }
    }
}
    