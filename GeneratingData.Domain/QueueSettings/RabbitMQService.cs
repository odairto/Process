using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Process.Domain.QueueSettings
{
    public class RabbitMQService
    {
        private readonly RabbitMQSettings _settings;
        private IModel _channel;

        public RabbitMQService(IOptions<RabbitMQSettings> settings)
        {
            _settings = settings.Value;
            CreateConnection();
            QueueConfiguration();
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(_settings.Exchange, _settings.RoutingKey, null, body);

            Console.WriteLine(message);
        }

        public void ProcessClientFromQueue()
        {
            var consumer = new EventingBasicConsumer(_channel);
         

            consumer.Received += (model, ea) =>
            {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: _settings.Queue, autoAck: false, consumer: consumer);
        }

        private void CreateConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password,
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();           
        }

        private void QueueConfiguration()
        {
            _channel.ExchangeDeclare(exchange: _settings.Exchange, type: ExchangeType.Direct);

            _channel.QueueDeclare(queue: _settings.Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: _settings.Queue, exchange: _settings.Exchange, routingKey: _settings.RoutingKey);
        }
    }
}
