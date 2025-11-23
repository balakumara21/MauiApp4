using MauiApp4.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

namespace MauiApp4.Shared
{
    public class TokenHandler: DelegatingHandler
    {

        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
         HttpRequestMessage request, CancellationToken cancellationToken)
        {
        

            var token = TokenStore.Token;

            //request.Headers.Add("X-API-KEY", _configuration["APIKey"]);

            if (!string.IsNullOrEmpty(token))
            {
                
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
