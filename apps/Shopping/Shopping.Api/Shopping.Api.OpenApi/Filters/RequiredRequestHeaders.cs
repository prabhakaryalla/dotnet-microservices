using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shopping.Api.OpenApi.Filters;

public class RequiredRequestHeaders : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }

        ControllerActionDescriptor? descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
        SetGlobalHeaders(operation, descriptor);
    }

    private void SetGlobalHeaders(OpenApiOperation operation, ControllerActionDescriptor descriptor)
    {

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "RequestId",
            Description = "Api operation transaction identifier",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema { Type = "string" },
            Examples = RequestIdExample
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-partner-name",
            Description = "Client Name",
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema { Type = "string" },
            Examples = PartnerNameExample
        });

    }

    private static IDictionary<string, OpenApiExample> RequestIdExample
    {
        get
        {
            return new Dictionary<string, OpenApiExample>
            {
                {
                    "GUID",
                    new OpenApiExample 
                    {
                        Value = new Microsoft.OpenApi.Any.OpenApiString(Guid.NewGuid().ToString())
                    }
                }
            };
        }
    }

    private static IDictionary<string, OpenApiExample> PartnerNameExample
    {
        get
        {
            return new Dictionary<string, OpenApiExample>
            {
                {
                    "Test",
                    new OpenApiExample
                    {
                        Value = new Microsoft.OpenApi.Any.OpenApiString("TEST")
                    }
                }
            };
        }
    }
}
