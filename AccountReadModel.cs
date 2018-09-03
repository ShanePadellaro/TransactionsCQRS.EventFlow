using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    [ElasticsearchType(IdProperty = "Id", Name = "account")]
    public class AccountReadModel : IReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>
    {
        [Number(
            NumberType.Integer,
            Name = "Balance",
            Index = false)]
        public long Balance { get; set; }

//        public List<TransactionReadModel> Transacations { get; private set; }
        [Keyword(
            Index = true)]
        public string Id { get; set; }


        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
//            if(Transacations == null)
//                Transacations = new List<TransactionReadModel>();
//            
//            Transacations.Add(new TransactionReadModel(domainEvent.AggregateEvent.Amount, TransactionType.Credit,
//                Balance));
            Id = domainEvent.AggregateIdentity.Value;
            Balance += domainEvent.AggregateEvent.Amount;
        }


        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent> domainEvent)
        {
//            if(Transacations == null)
//                Transacations = new List<TransactionReadModel>();
//            
//            Transacations.Add(new TransactionReadModel(domainEvent.AggregateEvent.Amount, TransactionType.Debit,
//                Balance));
            Id = domainEvent.AggregateIdentity.Value;
            Balance -= domainEvent.AggregateEvent.Amount;
        }
    }
}