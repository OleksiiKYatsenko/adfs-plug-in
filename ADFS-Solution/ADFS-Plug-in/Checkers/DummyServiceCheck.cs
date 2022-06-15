using ADFS_Plug_in.Interfaces;

namespace ADFS_Plug_in.Checkers
{
    internal class DummyServiceCheck : IChecker
    {
        private readonly IServiceClient _dummyServiceClient;

        public DummyServiceCheck(IServiceClient dummyServiceClient)
        {
            _dummyServiceClient = dummyServiceClient;
        }

        public async Task<bool> CheckAsync()
        {
            var result = await _dummyServiceClient.GetServiceResponse();
            return result;
        }
    }
}
