using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Shopping.Api.Controllers.Polices.Requirements;

namespace Shopping.Api.Controllers.Polices.Handlers;

public class ClientNameHandler : AuthorizationHandler<ClientNameRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ClientNameHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientNameRequirement requirement)
    {
        if (requirement.HasClientName(_httpContextAccessor))
        {
            context.Succeed(requirement);
        }
        else
        {
            throw new UnauthorizedAccessException("Unable to authenticate client.");
        }
        return Task.CompletedTask;
    }
}

