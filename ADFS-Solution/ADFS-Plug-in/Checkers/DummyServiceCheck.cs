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

        public string Check(string userName)
        {
            var result = _dummyServiceClient.GetServiceResponse();
            return result ? userName : string.Empty;
        }
    }
}
