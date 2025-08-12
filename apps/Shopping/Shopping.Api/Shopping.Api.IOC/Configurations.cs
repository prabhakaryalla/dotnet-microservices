using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Api.Contracts.Configuration;
using Shopping.Api.Contracts.Interfaces;
using Shopping.Api.Controllers.Filters;
using Shopping.Api.OpenApi.Models;

namespace Shopping.Api.IOC;

public static class Configurations
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();


        //services.Configure<ConfigSettings>(configuration);
        //services.AddHttpClient<IConfigurationService, ConfigurationService>();
        //services.AddSingleton<IAppConfiguration, AppConfiguration>();
        //services.AddSingleton<IAppConfiguration<BaseConfiguration>, BaseConfiguration>();


        //services.Configure<ConfigurationSettings>();



        services.AddScoped<IClientConfiguration, ClientConfiguration>();
        services.AddScoped<TrackPerformanceFilter>();
        services.Configure<SwaggerSchemas>(configuration);

        services.AddMemoryCache();

        return services;
    }
}
