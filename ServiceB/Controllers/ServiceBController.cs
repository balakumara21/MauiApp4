using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceBController : ControllerBase
    {
        

        private readonly ILogger<ServiceBController> _logger;

        public ServiceBController(ILogger<ServiceBController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ServiceB")]
        public string Get()
        {
            return "ServiceB";
        }
    }
}
