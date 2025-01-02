using Asp.Versioning;

namespace Projects.Filters;

using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

public class SwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var version = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .OfType<ApiVersionAttribute>()
            .FirstOrDefault()?.Versions.ToString();

        if (!string.IsNullOrEmpty(version))
        {
            operation.Description += $" (API Version: {version})";
        }
    }
}
