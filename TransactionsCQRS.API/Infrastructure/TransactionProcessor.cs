using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TransactionsCQRS.API.Infrastructure
{
    public class TransactionProcessor : RabbitConsumer
    {
        private readonly MongoDbClient _dbClient;
        private readonly RabbitProducer _producer;

        public TransactionProcessor(IConfigurationRoot configurationRoot)
            : base(configurationRoot, "transactionExchange", "transactions", "transactions.new")
        {
            _dbClient = new MongoDbClient(configurationRoot["MongoDbConnectionString"]);
            _producer = new RabbitProducer(configurationRoot);
        }

        protected override async Task<bool> Handle(string message)
        {
            try
            {
                await _dbClient.Create(message);
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