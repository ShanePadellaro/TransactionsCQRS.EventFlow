using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Core;
using EventFlow.Extensions;
using EventFlow.Queries;

namespace TransactionsCQRS.EventFlow
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var resolver = EventFlowOptions.New.AddEvents(typeof(AccountCreditedEvent))
                .AddEvents(typeof(AccountDebitedEvent))
                .AddCommands(typeof(CreditAccountCommand))
                .AddCommandHandlers(typeof(DebitAccountCommandHandler))
                .AddCommandHandlers(typeof(CreditAccountCommandHandler))
                .UseInMemoryReadStoreFor<AccountReadModel>().CreateResolver();

            var id = AccountId.New;
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