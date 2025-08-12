using Microsoft.AspNetCore.Http;
using Shopping.Api.Contracts.Domain;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Shopping.Api.Middlewares;

public class GlobalErrorHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;


    public GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }


    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        _logger.LogError(ex, "Exception caught in the global error handler");
        OperationResult resp;
        switch (ex)
        {
            case UnauthorizedAccessException unauthorized:
                resp = new OperationResult(((int)HttpStatusCode.Unauthorized).ToString(), ex.Message);
                break;
            case ArgumentException argument:
                resp = new OperationResult(((int)HttpStatusCode.BadRequest).ToString(), ex.Message);
                break;
            default:
                string message = "An internal server error has occurred. Please try again later.";
#if DEBUG
                message = ex.ToString();
#endif
                resp = new OperationResult(((int)HttpStatusCode.InternalServerError).ToString(), message);
                break;
        }


        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = Convert.ToInt32(resp.Code);


        OperationResult[] operationResults = { resp };
        StatusResponse result = new StatusResponse { OperationResults = operationResults };


        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } }));
    }
}

