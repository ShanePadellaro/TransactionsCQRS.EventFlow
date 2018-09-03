using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Core;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using EventFlow.Queries;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var resolver = EventFlowOptions.New
                .ConfigureElasticsearch(new Uri("http://localhost:9200/"))
                .ConfigureMongoDb("mongodb://localhost", "test")
                .UseMongoDbEventStore()
                .AddEvents(typeof(AccountCreditedEvent))
                .AddEvents(typeof(AccountDebitedEvent))
                .AddCommands(typeof(CreditAccountCommand))
                .AddCommandHandlers(typeof(DebitAccountCommandHandler))
                .AddCommandHandlers(typeof(CreditAccountCommandHandler))
                .UseElasticsearchReadModel<AccountReadModel>()
                .AddQueryHandler<GetAccountByIdQueryHandler, GetAccountByIdQuery, AccountReadModel>()
                .CreateResolver();



//  Mapping needed for first run
//            var _elasticClient = resolver.Resolve<IElasticClient>();
//            _elasticClient.CreateIndex("account", c => c
//                .Settings(s => s
//                    .NumberOfShards(1)
//                    .NumberOfReplicas(0))
//                .Mappings(m => m
//                    .Map<AccountReadModel>(d => d
//                        .AutoMap())));

            var id = new AccountId("account-59f5a0b8-61f3-47b2-87b9-5ad50c4ddc6d");
            var commandBus = resolver.Resolve<ICommandBus>();

            var result = await commandBus.PublishAsync(new CreditAccountCommand(id, 100),
                CancellationToken.None);

            result = await commandBus.PublishAsync(new CreditAccountCommand(id, 300),
                CancellationToken.None);

            result = await commandBus.PublishAsync(new DebitAccountCommand(id, 300),
                CancellationToken.None);

            result = await commandBus.PublishAsync(new CreditAccountCommand(id, 300),
                CancellationToken.None);

            var queryProcessor = resolver.Resolve<IQueryProcessor>();

            var accountReadModel = await queryProcessor.ProcessAsync(
                    new GetAccountByIdQuery(id), 
                    CancellationToken.None)
                .ConfigureAwait(false);
        }
    }
}