using Process.Domain;

namespace Process
{
    public class Worker : BackgroundService
    {
        private readonly Execute _execute;

        public Worker(Execute execute)
        {
            _execute = execute;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _execute.ExecuteProcess();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
