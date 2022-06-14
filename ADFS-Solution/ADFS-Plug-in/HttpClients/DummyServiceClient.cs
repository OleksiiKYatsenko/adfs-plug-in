using ADFS_Plug_in.Interfaces;
using Newtonsoft.Json;

namespace ADFS_Plug_in.HttpClients
{
    internal class DummyServiceClient : IServiceClient
    {
        private const string url = "https//:dummyservice.com";
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
                var responseString = _client.GetStringAsync(url).GetAwaiter().GetResult();
                result = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch(JsonException ex)
            {
                _logger.LogWarning($"Parsing problems: {ex.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogWarning($"Unexpected problems: {ex.Message}");
            }
            return result;
        }
    }
}
