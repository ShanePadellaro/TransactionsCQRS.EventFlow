using EventFlow.Queries;
using TransactionsCQRS.EventFlow.Domain.Account.ReadModels;

namespace TransactionsCQRS.EventFlow.Domain.Account.Queries
{
    public class GetAccountByIdQuery : IQuery<AccountReadModel>
    {
        public AccountId Id { get; }

        public GetAccountByIdQuery(AccountId id)
        {
            Id = id;
        }
    }
}