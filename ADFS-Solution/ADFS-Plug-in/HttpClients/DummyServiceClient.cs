using ADFS_Plug_in.Interfaces;
using Newtonsoft.Json;

namespace ADFS_Plug_in.HttpClients
{
    internal class DummyServiceClient : IServiceClient
    {
        private readonly HttpClient _client;
        private readonly ILogManager _logger;
        public DummyServiceClient(HttpClient httpClient, ILogManager logger)
        {
            _client = httpClient;
            _logger = logger;
        }
        public bool GetServiceResponse()
        {
            bool result = false;
            try
            {
                var responseString = _client.GetStringAsync("").GetAwaiter().GetResult();
                result = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch(JsonException ex)
            {
                _logger.LogError($"Parsing problems: {ex.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Unexpected problems: {ex.Message}");
            }
            return result;
        }
    }
}
