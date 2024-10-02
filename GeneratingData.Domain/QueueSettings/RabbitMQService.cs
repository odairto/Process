using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratingData.Domain.QueueSettings
{
    public class RabbitMQService
    {
        private readonly RabbitMQSettings _settings;

        public RabbitMQService(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;
            QueueConfiguration();
        }

        public void SendMessage(string message)
        {

            var factory = CreateConnection();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(_settings.Exchange, _settings.RoutingKey, null, body);

            Console.WriteLine(message);
        }

        private ConnectionFactory CreateConnection()
        {
            return new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password,
            };
        }

        private void QueueConfiguration()
        {
            var factory = CreateConnection();
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: _settings.Exchange, type: ExchangeType.Direct);

            channel.QueueDeclare(queue: _settings.Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind(queue: _settings.Queue, exchange: _settings.Exchange, routingKey: _settings.RoutingKey);
        }
    }
}
