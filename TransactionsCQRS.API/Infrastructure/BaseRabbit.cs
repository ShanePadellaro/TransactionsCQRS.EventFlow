using System;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace TransactionsCQRS.API.Infrastructure
{
    public abstract class BaseRabbit
    {
        protected ConnectionFactory CreateFactory(IConfigurationRoot config)
        {
            var port = config["RabbitMqPort"];
            var host = config["RabbitMqHost"];
            var username = config["RabbitMqUsername"];
            var password = config["RabbitMqPassword"];

            if (port == null)
            {
                throw new NullReferenceException("The rabbitmq port is missing from the web config");
            }
            if (host == null)
            {
                throw new NullReferenceException("The rabbitmq host is missing from the web config");
            }
            if (username == null)
            {
                throw new NullReferenceException("The rabbitmq username is missing from the web config");
            }
            if (password == null)
            {
                throw new NullReferenceException("The rabbitmq password is missing from the web config");
            }

            if (!int.TryParse(port, out var portNumber))
            {
                throw new ArgumentException("The rabbitmq port must be a number.");
            }
            
            return new ConnectionFactory
            {
                HostName = host,
                Port = portNumber,
                UserName = username,
                Password = password,
                DispatchConsumersAsync = true //Dont remove
            };
        }
    }
}