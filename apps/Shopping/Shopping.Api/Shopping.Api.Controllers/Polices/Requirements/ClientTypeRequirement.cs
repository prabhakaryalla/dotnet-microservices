using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shopping.Api.Contracts.Configuration;
using Shopping.Api.Contracts.Domain;

namespace Shopping.Api.Controllers.Polices.Requirements;

public class ClientTypeRequirement : IAuthorizationRequirement
{
    internal bool HasClientType(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor == null) return false;
        string clientType = httpContextAccessor.HttpContext.Request.Headers[HeaderType.ClientType];


        return !string.IsNullOrEmpty(clientType);
    }
}
