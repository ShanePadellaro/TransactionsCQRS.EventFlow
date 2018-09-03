using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Core;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;
using EventFlow.MsSql;
using EventFlow.MsSql.EventStores;
using EventFlow.MsSql.Extensions;
using EventFlow.Queries;
using Nest;

namespace TransactionsCQRS.EventFlow
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var resolver = EventFlowOptions.New
//                .ConfigureElasticsearch(new Uri("http://localhost:9200"))
//                .ConfigureElasticsearch()
                .ConfigureMsSql(MsSqlConfiguration.New
                    .SetConnectionString(@"Server=localhost;Database=testDB;User Id=sa;Password=Passw0rd"))
                .UseMssqlEventStore()
                .AddEvents(typeof(AccountCreditedEvent))
                .AddEvents(typeof(AccountDebitedEvent))
                .AddCommands(typeof(CreditAccountCommand))
                .AddCommandHandlers(typeof(DebitAccountCommandHandler))
                .AddCommandHandlers(typeof(CreditAccountCommandHandler))
                .UseMssqlReadModel<AccountReadModel>()
                .UseMssqlReadModel<TransactionReadModel>()
                .CreateResolver();
            
            var msSqlDatabaseMigrator = resolver.Resolve<IMsSqlDatabaseMigrator>();
            EventFlowEventStoresMsSql.MigrateDatabase(msSqlDatabaseMigrator);
            
//            var elasticClient = resolver.Resolve<IElasticClient>();
//            var elasticClient = new ElasticClient();
//
//            
//            elasticClient.CreateIndex("test", c => c
//                .Settings(s => s
//                    .NumberOfShards(1)
//                    .NumberOfReplicas(0))
//                .Mappings(m => m
//                    .Map<AccountReadModel>(d => d
//                        .AutoMap())));

            var id = new AccountId("account-aca15bbb-55a7-416a-a7b3-370996e93297");
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
                    new ReadModelByIdQuery<AccountReadModel>(id),
                    CancellationToken.None)
                .ConfigureAwait(false);
        }
    }
}