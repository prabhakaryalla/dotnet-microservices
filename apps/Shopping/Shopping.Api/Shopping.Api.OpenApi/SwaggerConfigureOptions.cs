using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Shopping.Api.OpenApi.Filters;
using Shopping.Api.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shopping.Api.OpenApi;

public class SwaggerConfigureOptions : IConfigureNamedOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private readonly ApiInfo _apiInfo;

    public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider, IOptions<ApiInfo> apiInfo)
    {
        _provider = provider;
        _apiInfo = apiInfo.Value;
    }

    /// <summary>
    /// Configure each API discovered for Swagger Documentation
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        //options.ExampleFilters();
        options.EnableAnnotations();

        options.OperationFilter<SwaggerDefaultsFilter>();
        options.SchemaFilter<NamespaceSchemaFilter>();
        options.OperationFilter<RequiredRequestHeaders>();


        // add swagger document for every API version discovered
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
        }

        XmlCommentsFiles.ForEach(file => options.IncludeXmlComments(file, includeControllerXmlComments: true));
        options.CustomSchemaIds(x => x.FullName);
    }

    /// <summary>
    /// Configure Swagger Options. Inherited from the Interface
    /// </summary>
    /// <param name="name"></param>
    /// <param name="options"></param>
    public void Configure(string name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    /// <summary>
    /// Create information about the version of the API
    /// </summary>
    /// <param name="description"></param>
    /// <returns>Information about the API</returns>
    private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
    {
        var openApiInfo = new OpenApiInfo
        {
            Title = _apiInfo.Title,
            Version = description.ApiVersion.ToString(),
            Description = _apiInfo.Description,
            TermsOfService = new Uri(_apiInfo.TermsOfService),
            Contact = new OpenApiContact
            {
                Name = _apiInfo.Contact.Name,
                Email = _apiInfo.Contact.Email,
                Url = new Uri(_apiInfo.Contact.Url),
            },
            License = new OpenApiLicense
            {
                Name = _apiInfo.License.Name,
                Url = new Uri(_apiInfo.License.Url),
            }
        };

        if (description.IsDeprecated)
        {
            openApiInfo.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
        }
        return openApiInfo;

    }

    private static List<string> XmlCommentsFiles => Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
}