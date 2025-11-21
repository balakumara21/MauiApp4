
namespace APIGateway.API.ServiceA
{

    public class ServiceA_API : IServiceA_API
    {
        private readonly HttpClient _httpClient;

       private readonly ILogger <ServiceA_API> _logger;

        private readonly IConfiguration _configuration;

        public ServiceA_API(HttpClient httpClient, ILogger<ServiceA_API> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> GetServiceA()
        {
            var endpoint = "api/v1/ServiceA/a/GetServiceA";
            var response= await _httpClient.GetAsync(endpoint);
            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> GetServiceA1()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetServiceA2()
        {
            throw new NotImplementedException();
        }
    }
}
