using Microsoft.Extensions.DependencyInjection;
using Shopping.Api.Contracts.Interfaces.Managers;
using Shopping.Api.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Api.IOC
{
    public static class Managers
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
            return services;
        }
    }
}
