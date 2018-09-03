using System.Collections.Generic;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores;
using EventFlow.ReadStores;

namespace TransactionsCQRS.EventFlow
{
    public class AccountReadModel :MssqlReadModel, IReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>
    {
        public long Balance { get; private set; }
//        public List<TransactionReadModel> Transacations { get; private set; }

        public void Apply(IReadModelContext context,
            IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
//            if(Transacations == null)
//                Transacations = new List<TransactionReadModel>();
//            
//            Transacations.Add(new TransactionReadModel(domainEvent.AggregateEvent.Amount, TransactionType.Credit,
//                Balance));
            
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
            
            Balance -= domainEvent.AggregateEvent.Amount;
        }
    }
}