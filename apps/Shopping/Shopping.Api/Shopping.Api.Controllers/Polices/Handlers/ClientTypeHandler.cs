using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shopping.Api.Controllers.Polices.Requirements;

namespace Shopping.Api.Controllers.Polices.Handlers;


public class ClientTypeHandler : AuthorizationHandler<ClientTypeRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ClientTypeHandler(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientTypeRequirement requirement)
    {
        if (requirement.HasClientType(_httpContextAccessor))
        {
            context.Succeed(requirement);
        }
        else
        {
            throw new ArgumentException("Missing or invalid Client Type.");
        }


        return Task.CompletedTask;
    }
}
