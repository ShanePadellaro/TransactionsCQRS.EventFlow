using EventFlow.Queries;

namespace TransactionsCQRS.EventFlow
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