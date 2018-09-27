using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace TransactionsCQRS.API.Infrastructure
{
    public class RabbitProducer : BaseRabbit
    {
        private readonly ConnectionFactory _factory;
        
        public RabbitProducer(IConfigurationRoot configManager)
        {
            _factory = CreateFactory(configManager);
        }
        
        public void Send(string exchange, string message, string queue, string routingKey)
        {
            using(var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange, "topic");
                channel.QueueDeclare(queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                
                channel.QueueBind(queue, exchange, routingKey);

                var body = Encoding.UTF8.GetBytes(message);

                var props = channel.CreateBasicProperties();
                props.DeliveryMode = 2; // 1 = non-persistent; 2 = persistent
                
                
                channel.BasicPublish(exchange: exchange,
                    routingKey: routingKey,
                    basicProperties: props,
                    body: body);
                
            }
        }
    }
}