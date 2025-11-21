using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIGateway.Filter
{
    public class InternalTokenHeaderFilter : IOperationFilter
    {
        private readonly string _internalToken;

        public InternalTokenHeaderFilter(IConfiguration configuration)
        {
            _internalToken = configuration["InternalToken"]; // from appsettings.json
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Internal-Token",
                In = ParameterLocation.Header,
                Description = $"Internal token (default: {_internalToken})",
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new Microsoft.OpenApi.Any.OpenApiString(_internalToken)
                }
            });
        }
    }
}
