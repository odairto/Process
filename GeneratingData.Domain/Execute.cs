using GeneratingData.Domain.Services;

namespace GeneratingData.Domain
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
            _clientService.GenerateClient();
        }
    }

}
