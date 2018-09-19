using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Elasticsearch.ReadStores;
using EventFlow.Queries;
using MongoDB.Driver;

namespace TransactionsCQRS.EventFlow.Queries
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

            var account = await collection.Find(x => x._id == query.Id.ToString()).FirstOrDefaultAsync(cancellationToken);
            
            return account;
        }
    }
    
    public class GetFeesByCompanyIdQueryHandler :
        IQueryHandler<GetFeesByCompanyIdQuery,List<TransactionReadModel>>
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

            var account = await collection.FindAsync(x => x.Transaction.TransactionItems.Any(p=>p.SubFees.Any(d=>d.CompanyId == query.CompanyId)), cancellationToken: cancellationToken);
            
            return account.ToList();
        }
    }
}