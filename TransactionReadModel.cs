using System.ComponentModel.DataAnnotations.Schema;
using EventFlow.Aggregates;
using EventFlow.MsSql.ReadStores;
using EventFlow.ReadStores;

namespace TransactionsCQRS.EventFlow
{
    [Table("ReadModel-Transaction")]
    public class TransactionReadModel:MssqlReadModel, IReadModel, IAmReadModelFor<AccountAggregate, AccountId, AccountCreditedEvent>,
        IAmReadModelFor<AccountAggregate, AccountId, AccountDebitedEvent>
    {
        public long Amount { get; set; }
        public string Type { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountCreditedEvent> domainEvent)
        {
            Amount = domainEvent.AggregateEvent.Amount;
            Type = "Credit";
        }

        public void Apply(IReadModelContext context, IDomainEvent<AccountAggregate, AccountId, AccountDebitedEvent> domainEvent)
        {
            Amount = domainEvent.AggregateEvent.Amount;
            Type = "Debit";
        }
    }
}