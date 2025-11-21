using System.Security.Claims;

namespace APIGateway.Middleware
{
    public class JWTTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _internalToken; // your internal token

        public JWTTokenMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _internalToken = config["InternalToken"]; // set in appsettings.json
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the request has your internal token header
            if (context.Request.Headers.TryGetValue("X-Internal-Token", out var token))
            {
                if (token == _internalToken)
                {
                    // Create a fake authenticated user for internal call
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, "InternalUser") };
                    var identity = new ClaimsIdentity(claims, "Internal");
                    context.User = new ClaimsPrincipal(identity);
                }
            }

            await _next(context);
        }
    }
}

