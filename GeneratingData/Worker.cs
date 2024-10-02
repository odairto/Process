using GeneratingData.Domain;

namespace GeneratingData
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly Execute _execute;

        public Worker(ILogger<Worker> logger, Execute execute)
        {
            _logger = logger;
            _execute = execute;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("{time} Generating clients", DateTimeOffset.Now);
                //}

                _execute.ExecuteProcess();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
