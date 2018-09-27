using EventFlow.Queries;
using TransactionsCQRS.API.Domain.Account.ReadModels;

namespace TransactionsCQRS.API.Domain.Account.Queries
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