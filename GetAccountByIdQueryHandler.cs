using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Elasticsearch.ReadStores;
using EventFlow.Queries;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    public class GetAccountByIdQueryHandler :
        IQueryHandler<GetAccountByIdQuery, AccountReadModel>
    {
        private readonly IElasticClient _elasticClient;
        private readonly IReadModelDescriptionProvider _readModelDescriptionProvider;

        public GetAccountByIdQueryHandler(
            IElasticClient elasticClient,
            IReadModelDescriptionProvider readModelDescriptionProvider)
        {
            _elasticClient = elasticClient;
            _readModelDescriptionProvider = readModelDescriptionProvider;
        }

        public async Task<AccountReadModel> ExecuteQueryAsync(
            GetAccountByIdQuery query,
            CancellationToken cancellationToken)
        {
            var readModelDescription = _readModelDescriptionProvider.GetReadModelDescription<AccountReadModel>();
            var getResponse = await _elasticClient.GetAsync<AccountReadModel>(
                    query.Id.Value,
                    d => d
                        .Index(readModelDescription.IndexName.Value)
                        .RequestConfiguration(c => c
                            .AllowedStatusCodes((int) HttpStatusCode.NotFound)),
                    cancellationToken)
                .ConfigureAwait(false);

            return getResponse != null && getResponse.Found
                ? getResponse.Source
                : null;
        }
    }
}