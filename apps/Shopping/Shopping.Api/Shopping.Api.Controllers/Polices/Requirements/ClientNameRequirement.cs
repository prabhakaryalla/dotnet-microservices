using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shopping.Api.Contracts.Domain;

namespace Shopping.Api.Controllers.Polices.Requirements;

public class ClientNameRequirement : IAuthorizationRequirement
{
    internal bool HasClientName(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor == null) return false;
        string clientName = httpContextAccessor.HttpContext.Request.Headers[HeaderType.ClientName];
        return !string.IsNullOrEmpty(clientName);
    }
}
