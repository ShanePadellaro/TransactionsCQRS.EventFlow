using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using MongoDB.Driver;
using System.Linq;
using TransactionsCQRS.API.Domain.Account.ReadModels;

namespace TransactionsCQRS.API.Domain.Account.Queries
{
    public class GetFeesByCompanyIdQueryHandler :
        IQueryHandler<GetFeesByCompanyIdQuery, List<TransactionReadModel>>
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;

        public GetFeesByCompanyIdQueryHandler(
            IMongoDatabase mongoDatabase,
            IReadModelDescriptionProvider readModelDescriptionProvider)
        {
            _mongoDatabase = mongoDatabase;
            _readModelDescriptionProvider = readModelDescriptionProvider;
        }


        public async Task<List<TransactionReadModel>> ExecuteQueryAsync(GetFeesByCompanyIdQuery query,
            CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<TransactionReadModel>("TransactionReadModel");

            var account = await collection.FindAsync(
                x => x.TransactionItems.Any(p => p.SubFees.Any(d => d.CompanyId == query.CompanyId)),
                cancellationToken: cancellationToken);

            return account.ToList();
        }
    }
}