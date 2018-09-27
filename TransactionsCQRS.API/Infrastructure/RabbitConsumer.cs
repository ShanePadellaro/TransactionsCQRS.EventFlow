using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TransactionsCQRS.API.Infrastructure
{
    public abstract class RabbitConsumer : BaseRabbit, IHostedService
    {
        private readonly ConnectionFactory _factory;
        private readonly string _exchange;
        private readonly string _queueName;
        private readonly string _routingKey;

        protected RabbitConsumer(IConfigurationRoot configManager, string exchange, string queueName, string routingKey)
        {
            _exchange = exchange;
            _queueName = queueName;
            _routingKey = routingKey;
            _factory = CreateFactory(configManager);
        }
        
        protected abstract Task<bool> Handle(string message);
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();
            
            channel.ExchangeDeclare(_exchange, "topic");
            channel.QueueDeclare(_queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind(queue: _queueName,
                exchange: _exchange,
                routingKey: _routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);
            
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var isSuccess = await Handle(message).ConfigureAwait(false);

                if (isSuccess)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    channel.BasicReject(ea.DeliveryTag, false);
                }
            };
            
            channel.BasicConsume(queue: _queueName,
                autoAck: false,
                consumer: consumer);
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}