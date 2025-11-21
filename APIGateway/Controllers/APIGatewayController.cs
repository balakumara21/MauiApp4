using APIGateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class APIGatewayController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public APIGatewayController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet(Name = "GetServiceA")]
        public async Task<string> Get()
        {
            return await _commonService.GetServiceA();
        }
    }
}
