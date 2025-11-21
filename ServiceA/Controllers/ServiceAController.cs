using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ServiceAController : ControllerBase
    {
       

        private readonly ILogger<ServiceAController> _logger;

        public ServiceAController(ILogger<ServiceAController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("a/GetServiceA")]
        public string GetServiceA()
        {
            return "ServiceA";
            
        }


        [HttpGet]
        [Route("b/GetServiceA1")]
        public string GetServiceA1()
        {
            return "ServiceA1";

        }


        [HttpGet]
        [Route("c/GetServiceA2")]
        public string GetServiceA2()
        {
            return "ServiceA2";

        }
    }
}
