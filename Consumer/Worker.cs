using Process.Domain.QueueSettings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Data.Common;
using System.Threading.Channels;
using Process;

namespace Consumer
{
    public class Worker : BackgroundService
    {
        private readonly Execute _execute;

        public Worker(Execute execute)
        {
            _execute = execute;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _execute.ExecuteProcess();
            return Task.CompletedTask;
        }
    }
}
