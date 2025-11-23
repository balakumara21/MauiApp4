using DAO.Models;
using DAO.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace APIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost(Name = "PostLoginData")]

        public async Task<IActionResult> PostLoginData(UserInfo userInfo)
        {
            var user=await _loginService.GetUser(userInfo);
            if (user != null)
            {
                
                return Ok(true);
            }
            else
            {
                
                return Unauthorized(false);
            }
        }
    }
}
