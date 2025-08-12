using Microsoft.AspNetCore.Builder;
using Shopping.Api.Middlewares;

namespace Shopping.Api.IOC;

public static class Middlewares
{
    public static WebApplication UseClientConfiguration(this WebApplication app)
    {
        app.UseMiddleware<ClientConfigurationMiddleware>();
        return app;
    }


    public static WebApplication UseGlobalErrorHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalErrorHandler>();
        return app;
    }
}
