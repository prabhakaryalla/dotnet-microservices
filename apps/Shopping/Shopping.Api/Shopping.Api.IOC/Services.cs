using Microsoft.Extensions.DependencyInjection;
using Shopping.Api.Contracts.Services;
using Shopping.Api.Services.Products;

namespace Shopping.Api.IOC
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
