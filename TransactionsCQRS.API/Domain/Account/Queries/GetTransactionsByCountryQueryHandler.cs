using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using MongoDB.Driver;
using TransactionsCQRS.API.Domain.Account.ReadModels;

namespace TransactionsCQRS.API.Domain.Account.Queries
{
    public class GetTransactionsByCountryQueryHandler :
        IQueryHandler<GetTransactionsByCountry, IList<TransactionReadModel>>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public GetTransactionsByCountryQueryHandler(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }


        public async Task<IList<TransactionReadModel>> ExecuteQueryAsync(GetTransactionsByCountry query,
            CancellationToken cancellationToken)
        {
            var collection = _mongoDatabase.GetCollection<TransactionReadModel>("TransactionReadModel");

            var transactions = await collection.Find(x =>
                    x.CountryCode.ToLower() == query.Countrycode.ToLower()
                    && x.BillingDate >= query.From 
                    && x.BillingDate <= query.To
                )
                .Skip(query.Skip)
                .Limit(query.Take).ToListAsync(cancellationToken: cancellationToken);


            return transactions;
        }
    }
}