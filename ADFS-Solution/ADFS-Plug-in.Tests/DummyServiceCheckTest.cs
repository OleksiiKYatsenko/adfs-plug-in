using ADFS_Plug_in.Checkers;
using ADFS_Plug_in.HttpClients;
using ADFS_Plug_in.Interfaces;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace ADFS_Plug_in.Tests
{
    public class DummyServiceCheckTest : TestClientBase
    {
        internal readonly IServiceClient serviceClient;
        internal readonly IChecker dummyCheck;
        public DummyServiceCheckTest()
        {
            serviceClient = new DummyServiceClient(_client, _logger);
            dummyCheck = new DummyServiceCheck(serviceClient);
            
        }

        [Fact]
        public async Task ServiceCheckShoulReturn_Empty()
        {
            ConfigureServer(false);
            ConfigureClient();

            var checkResult = dummyCheck.Check(UserName);

            Assert.NotEqual(UserName, checkResult);
            Assert.Equal(string.Empty, checkResult);
        }
        [Fact]
        public async Task ServiceCheckShoulReturn_UserName()
        {
            ConfigureServer(true);
            ConfigureClient();            

            var checkResult = dummyCheck.Check(UserName);

            Assert.Equal(UserName, checkResult);
        }
    }
}
