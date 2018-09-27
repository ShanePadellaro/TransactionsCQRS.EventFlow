using System;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionsCQRS.API.Domain.Account;
using TransactionsCQRS.API.Domain.Account.Commands;
using TransactionsCQRS.API.Domain.Account.ValueObjects;

namespace TransactionsCQRS.API.Infrastructure
{
    public class TransactionProcessor : RabbitConsumer
    {
        private readonly RabbitProducer _producer;

        public TransactionProcessor(IConfigurationRoot configurationRoot)
            : base(configurationRoot, "transactionExchange", "transactions", "transactions.new")
        {
            _producer = new RabbitProducer(configurationRoot);
        }

        protected override async Task<bool> Handle(string message)
        {
            try
            {
                var transaction =  Newtonsoft.Json.JsonConvert.DeserializeObject<Transaction>(message);
                var commandBus = Startup.ServiceProvider.GetService<ICommandBus>();

                var command = new CreditAccountCommand(new AccountId("account-439fa12b-18ac-4c82-87c6-ddc74f591284"),
                    transaction);

                var result = await commandBus.PublishAsync(command,CancellationToken.None);
                
            }
            catch (Exception exception)
            {
                _producer.Send("transactionExchange", 
                    $"Message: {message} \nException:{exception.Message}", 
                    "transactions.failed",
                    "transactions.failed");
                return false;
            }

            return true;
        }
    }
}