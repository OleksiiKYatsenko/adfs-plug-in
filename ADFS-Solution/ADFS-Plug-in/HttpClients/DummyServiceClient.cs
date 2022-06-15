using ADFS_Plug_in.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ADFS_Plug_in.HttpClients
{
    internal class DummyServiceClient : IServiceClient
    {
        private const string url = "https//:dummyservice.com";
        private readonly HttpClient _client;
        private readonly ILogger<DummyServiceClient> _logger;
        public DummyServiceClient(HttpClient httpClient, ILogger<DummyServiceClient> logger)
        {
            _client = httpClient;
            _logger = logger;
        }
        public async Task<bool> GetServiceResponse()
        {
            bool result = false;
            try
            {
                var responseString = await _client.GetStringAsync(url);
                result = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "Dummy service call exception");
            }
            return result;
        }
    }
}
