using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using ADFS_Plug_in.Interfaces;
using ADFS_Plug_in.HttpClients;

namespace ADFS_Plug_in.Tests
{
    public  class DummyServiceClientTests : TestClientBase
    {
        internal readonly IServiceClient serviceClient;
        public DummyServiceClientTests()
        {
            serviceClient = new DummyServiceClient(_client, _logger);
        }

        [Fact]
        public async Task ServiceClientShoulReturn_False()
        {
            ConfigureServer(false);
            ConfigureClient();

            var response = serviceClient.GetServiceResponse();

            Assert.False(response);
        }
        [Fact]
        public async Task ServiceClientShoulReturn_True()
        {
            ConfigureServer(true);
            ConfigureClient();

            var response = serviceClient.GetServiceResponse();

            Assert.True(response);
        }
    }
}