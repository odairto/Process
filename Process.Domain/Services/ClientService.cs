using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Faker;
using Process.Domain.Entities;
using Process.Domain.QueueSettings;
using RabbitMQ.Client;

namespace Process.Domain.Services
{
    public class ClientService
    {
        private readonly RabbitMQService _rabbitMQService;

        public ClientService(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public void GenerateClient()
        {
            var client = new Client()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name.FullName(),
                Phone = Phone.Number()
            };

            _rabbitMQService.SendMessage(JsonSerializer.Serialize(client));
        }     
        

        public void ProcessClientFromQueue()
        {
            _rabbitMQService.ProcessClientFromQueue();            
        }
    }
}
