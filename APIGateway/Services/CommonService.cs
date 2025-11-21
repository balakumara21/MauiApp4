
using APIGateway.API.ServiceA;

namespace APIGateway.Services
{
    public class CommonService : ICommonService
    {
        private readonly IServiceA_API _serviceA_API;

        public CommonService(IServiceA_API serviceA_API)
        {
            _serviceA_API = serviceA_API;
        }

        public async Task<string> GetServiceA()
        {
          string ServiceA_API= await _serviceA_API.GetServiceA();
            return ServiceA_API;
        }
    }
}
