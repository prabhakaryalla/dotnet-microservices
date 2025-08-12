using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Api.Controllers.Filters;
using System.Text.Json.Serialization;

namespace Shopping.Api.IOC;

public static class ControllersGlobalConfiguration
{
    public static IServiceCollection AddControllersSettings(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<TrackPerformanceFilter>();
            options.Filters.Add(new ProducesAttribute("application/json"));
            options.Filters.Add(new ConsumesAttribute("application/json"));
        })
            .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });


        return services;
    }
}

