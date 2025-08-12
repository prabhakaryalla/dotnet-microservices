using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shopping.Api.Controllers.Polices.Requirements;

namespace Shopping.Api.Controllers.Polices.Handlers;

public class RequestIdHandler : AuthorizationHandler<RequestIdRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public RequestIdHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequestIdRequirement requirement)
    {


        if (requirement.HasRequestId(_httpContextAccessor))
        {
            context.Succeed(requirement);
        }
        else
        {
            throw new ArgumentException("Missing or invalid RequestId.");
        }


        return Task.CompletedTask;
    }
}

