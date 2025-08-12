using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Api.OpenApi.Models;
using Shopping.Api.OpenApi;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Shopping.Api.IOC;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services)
    {
        services.AddVersioning();


        IServiceProvider serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();
        services.Configure<ApiInfo>(configuration.GetSection("ApiInfo"));
        services.ConfigureOptions<SwaggerConfigureOptions>();


        services.AddSwaggerGen();
        return services;
    }


    public static WebApplication UseSwaggerSettings(this WebApplication app)
    {
        string pathBase = "/";
        app.UsePathBase(pathBase);
        app.UseSwagger(options =>
        {
            // options.SerializeAsV2 = true;
            options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase}" }
                    };
            });
        });



        app.UseSwaggerUI(options =>
        {
            IApiVersionDescriptionProvider provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"{pathBase}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                options.DocumentTitle = "Shopping Api";
            }
            //options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        });


        return app;
    }



    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });
        services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });


        return services;
    }
}
