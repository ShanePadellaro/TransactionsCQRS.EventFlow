using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using MongoDB.Driver;
using TransactionsCQRS.API.Domain.Account.ReadModels;

namespace TransactionsCQRS.API.Domain.Account.Queries
{
    public class GetAccountByIdQueryHandler :
        IQueryHandler<GetAccountByIdQuery, AccountReadModel>
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;

        public GetAccountByIdQueryHandler(
            IMongoDatabase mongoDatabase,
            IReadModelDescriptionProvider readModelDescriptionProvider)
        {
            _mongoDatabase = mongoDatabase;
            _readModelDescriptionProvider = readModelDescriptionProvider;
        }

        public async Task<AccountReadModel> ExecuteQueryAsync(GetAccountByIdQuery query,
            CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<AccountReadModel>("AccountReadModel");

            var account = await collection.Find(x => x._id == query.Id.ToString())
                .FirstOrDefaultAsync(cancellationToken);

            return account;
        }
    }
}