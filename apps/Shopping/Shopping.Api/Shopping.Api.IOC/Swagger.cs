using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Shopping.Api.IOC
{
    public static class Swagger
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Shopping Api",
                    Description = "An ASP.NET Core Web API for managing Shopping items",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact",
                        Url = new Uri("https://example.com/contact")
                    }
                });
            });
            return services;
        }
    }
}
