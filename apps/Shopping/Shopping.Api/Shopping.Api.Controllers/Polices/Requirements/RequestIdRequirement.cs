using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shopping.Api.Contracts.Domain;

namespace Shopping.Api.Controllers.Polices.Requirements;

public class RequestIdRequirement : IAuthorizationRequirement
{
    internal bool HasRequestId(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor == null) return false;
        return IsValidGuid(httpContextAccessor.HttpContext.Request.Headers[HeaderType.RequestId]);
    }

    private static bool IsValidGuid(string requestId)
    {
        if (string.IsNullOrWhiteSpace(requestId)) return false;
        if (Guid.TryParse(requestId, out Guid requestGuid))
        {
            if (requestGuid == Guid.Empty) return false;
            return true;
        }
        return false;
    }
}