using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.Commands;
using EventFlow.Core;
using EventFlow.Elasticsearch.Extensions;
using EventFlow.Extensions;
using EventFlow.MongoDB.Extensions;
using EventFlow.Queries;
using Nest;
using TransactionsCQRS.EventFlow.Domain.Account;
using TransactionsCQRS.EventFlow.Domain.Account.Commands;
using TransactionsCQRS.EventFlow.Domain.Account.Queries;
using TransactionsCQRS.EventFlow.Domain.Account.ReadModels;
using TransactionsCQRS.EventFlow.Domain.Account.ValueObjects;
using KeyValuePair = TransactionsCQRS.EventFlow.Domain.Account.ValueObjects.KeyValuePair;

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
                .UseMongoDbSnapshotStore()
                .AddSnapshots(Assembly.GetExecutingAssembly())
                .AddCommands(Assembly.GetExecutingAssembly(),x=>x.IsSubclassOf(typeof(Command<,>)))
                .AddEvents(Assembly.GetExecutingAssembly())
                .AddCommandHandlers(Assembly.GetExecutingAssembly())
                .UseMongoDbReadModel<AccountReadModel>()
                .RegisterServices(x=>x.RegisterType(typeof(TransactionReadModelLocator)))
                .UseMongoDbReadModel<TransactionReadModel,TransactionReadModelLocator>()
                .AddQueryHandlers(Assembly.GetExecutingAssembly())
//                .AddQueryHandler<GetAccountByIdQueryHandler, GetAccountByIdQuery, AccountReadModel>()
//                .AddQueryHandler<GetFeesByCompanyIdQueryHandler,GetFeesByCompanyIdQuery,List<TransactionReadModel>>()
                .AddQueryHandlers(Assembly.GetExecutingAssembly())
                .CreateResolver();
            
            var commandBus = resolver.Resolve<ICommandBus>();

            var accountDetails = new AccountDetails("ExternalId","Name", "GBR", "GBP", 50);
            var recipt = await commandBus.PublishAsync(new CreateAccountCommand(accountDetails),
                CancellationToken.None);

//            var id = new AccountId("account-b71862d8-5972-4359-87c3-b7c8d0f06dbb");
            var id = recipt.Id;
//            var props = new List<Dictionary<string, object>>();
//            props.Add(new Dictionary<string, object>() {{"item1", "value1"}});
//            props.Add(new Dictionary<string, object>() {{"item2", "value2"}});
            
            var subfee1 = new KeyValuePair("myKey","MyValue");
            var companyId = "b3e4bf26-c93b-41f6-adf1-27b85fa82c91";
            var subfee2 = new Fee(companyId, "MyLabel", "USD", "0.9", 0, 0);
            var subfees = new List<Fee>(){subfee2};
            var keyValueParis = new List<KeyValuePair>(){subfee1};
            var item = new TransactionItem(100,"B2C Renewal",1,keyValueParis,subfees);
            
            var transaction = new Transaction("T-00001",id.ToString(),"Transaction","B2C Renewal",100,0,DateTime.Now, 20,"GBR","GBP",new List<TransactionItem>(){item});
            
                var result = await commandBus.PublishAsync(new CreditAccountCommand(id,transaction),
                CancellationToken.None);
            
             result = await commandBus.PublishAsync(new CreditAccountCommand(id,transaction),
                CancellationToken.None);
                 result = await commandBus.PublishAsync(new CreditAccountCommand(id,transaction),
                CancellationToken.None);

            
            
            
//            result = await commandBus.PublishAsync(new DebitAccountCommand(id,transaction), 
//                CancellationToken.None);

//            var tasks = new List<Task>();
//            for (int i = 0; i < 100000; i++)
//            {
//                await commandBus.PublishAsync(new CreditAccountCommand(id, transaction),
//                    CancellationToken.None);
////                tasks.Add(task);
//            }

//            while (tasks.Count != 0)
//            {
//                var t = tasks.Take(6).ToList();
//                t.ForEach(d=>d.Start());
//                Task.WaitAll(t.ToArray());
//                tasks.RemoveAll(x => t.Contains(x));
//
//            }

            var queryProcessor = resolver.Resolve<IQueryProcessor>();

            var accountReadModel = await queryProcessor.ProcessAsync(
                    new GetAccountByIdQuery(id), 
                    CancellationToken.None)
                .ConfigureAwait(false);
            
            var transactions = await queryProcessor.ProcessAsync(
                    new GetFeesByCompanyIdQuery("b3e4bf26-c93b-41f6-adf1-27b85fa82c91"), 
                    CancellationToken.None)
                .ConfigureAwait(false);
            
            var taxes = await queryProcessor.ProcessAsync(
                    new GetTransactionsByCountry("gbr",DateTime.Now.AddDays(-1),DateTime.Now.AddDays(1),skip:1,take:2), 
                    CancellationToken.None)
                .ConfigureAwait(false);
        }
    }
}