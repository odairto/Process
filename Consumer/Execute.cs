using Process.Domain.Services;

namespace Process
{
    public class Execute
    {
        private readonly ClientService _clientService;

        public Execute(ClientService clientService)
        {
            _clientService = clientService;
        }

        public void ExecuteProcess()
        {
            _clientService.ProcessClientFromQueue();
        }
    }

}
