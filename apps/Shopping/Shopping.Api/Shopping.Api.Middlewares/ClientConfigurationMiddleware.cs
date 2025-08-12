using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Serilog;
using Shopping.Api.Contracts.Domain;
using Shopping.Api.Contracts.Interfaces;

namespace Shopping.Api.Middlewares;

public class ClientConfigurationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ClientConfigurationMiddleware> _logger;

    public ClientConfigurationMiddleware(RequestDelegate next, ILogger<ClientConfigurationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, IClientConfiguration clientConfiguration)
    {
        if (httpContext.Request.Headers.TryGetValue(HeaderType.ClientName, out StringValues clientName))
        {
            clientConfiguration.PartnerName = clientName.SingleOrDefault();
        }

        if (httpContext.Request.Headers.TryGetValue(HeaderType.ClientType, out StringValues lobCode))
        {
            if (!string.IsNullOrEmpty(lobCode)) clientConfiguration.ClientType = lobCode;
        }


        if (httpContext.Request.Headers.TryGetValue(HeaderType.RequestId, out StringValues requestId))
        {
            if (Guid.TryParse(requestId.SingleOrDefault(), out Guid requestGuid))
            {
                clientConfiguration.RequestId = requestGuid;
                httpContext.Response.Headers.Add(HeaderType.RequestId, requestGuid.ToString());
            }
            else
            {
                clientConfiguration.RequestId = Guid.NewGuid();
                httpContext.Response.Headers.Add(HeaderType.RequestId, clientConfiguration.RequestId.ToString());
            }
        }


        if (clientConfiguration.RequestId != null
            && Guid.TryParse(clientConfiguration.RequestId.ToString(), out _)
            && Guid.Empty != clientConfiguration.RequestId)
        {
            using (_logger.BeginScope("{@RequestId}", clientConfiguration.RequestId))
            {
                await _next.Invoke(httpContext);
            }
        }
        else
        {
            await _next.Invoke(httpContext);
        }

    }
}
