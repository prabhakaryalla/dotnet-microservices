using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shopping.Api.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shopping.Api.OpenApi.Filters;

public class NamespaceSchemaFilter : ISchemaFilter
{
    private static SwaggerSchemas _schemas;


    public NamespaceSchemaFilter(IOptions<SwaggerSchemas> options)
    {
        _schemas = options.Value;
    }


    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema is null)
        {
            throw new ArgumentNullException(nameof(schema));
        }


        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }


        schema.Title = $"{(_schemas.Interfaces.Contains(context.Type.Name) ? "Interface" : "Model")}.{context.Type.Name}";
    }
}
