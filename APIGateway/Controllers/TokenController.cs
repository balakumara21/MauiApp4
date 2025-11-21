using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace APIGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private const string ApiKey = "Bala";
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken([FromHeader(Name = "X-API-KEY")] string appCheckToken)
        {
            var jwtIssuer = _config.GetValue<string>("Jwt:Issuer");
            var jwtAudience = _config.GetValue<string>("Jwt:Audience");
            var jwtSecret = _config.GetValue<string>("Jwt:Secret");

            if (string.IsNullOrEmpty(appCheckToken) || appCheckToken != ApiKey)
                return Unauthorized("Invalid or missing App Check token");

            // Generate JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, "Bala"),
                new Claim("App", "Bala")
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }
    }
}
