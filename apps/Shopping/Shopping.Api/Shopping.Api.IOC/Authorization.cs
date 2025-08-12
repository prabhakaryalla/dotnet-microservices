using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Api.Controllers.Polices;
using Shopping.Api.Controllers.Polices.Handlers;
using Shopping.Api.Controllers.Polices.Requirements;

namespace Shopping.Api.IOC;

public static class Authorization
{
    public static IServiceCollection AddAuthorizations(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PolicyType.ClientName, policy => policy.Requirements.Add(new ClientNameRequirement()));
            options.AddPolicy(PolicyType.RequestId, policy => policy.Requirements.Add(new RequestIdRequirement()));
            options.AddPolicy(PolicyType.ClientType, policy => policy.Requirements.Add(new ClientTypeRequirement()));
        });


        services.AddSingleton<IAuthorizationHandler, ClientNameHandler>();
        services.AddSingleton<IAuthorizationHandler, RequestIdHandler>();
        services.AddSingleton<IAuthorizationHandler, ClientTypeHandler>();


        return services;
    }
}

