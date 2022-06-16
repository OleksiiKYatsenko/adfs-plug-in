using ADFS_Plug_in.Interfaces;
using ADFS_Plug_in.Loggers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ADFS_Plug_in.Tests
{
    public abstract class TestClientBase : IDisposable
    {
        protected readonly WireMockServer _server;
        protected readonly HttpClient _client;
        internal readonly ILogManager _logger;
        public static string UserName = "oleksii.yatsenko";

        public TestClientBase()
        {
            _client = new HttpClient();
            _server = WireMockServer.Start();
            _logger = new EventLogLogger();
        }

        public void ConfigureServer(object value)
        {
            _server
                .Given(Request.Create().WithPath("/check").UsingGet())
                .RespondWith(
                  Response.Create()
                    .WithStatusCode(200)
                    .WithBody(value.ToString()));
        }
        public void ConfigureClient()
        {
            _client.BaseAddress = new Uri($"{_server.Urls[0]}/check");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _server.Stop();
                _client.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
