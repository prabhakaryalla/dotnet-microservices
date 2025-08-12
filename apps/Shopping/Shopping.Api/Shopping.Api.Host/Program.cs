using Shopping.Api.IOC;

var builder = WebApplication.CreateBuilder(args);


//builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration))
//            .ConfigureAppConfiguration((ctx) => ctx.AddMachineConfig());


#region  Add services to the container.
IServiceCollection services = builder.Services;


services.AddHttpContextAccessor();
services.AddOptions();
services.AddConfigurations();
//services.AddAutoMapper();
services.AddServices();
services.AddManagers();
services.AddEndpointsApiExplorer();
services.AddSwaggerSettings();
services.AddControllersSettings();
services.AddAuthorizations();



services.Configure<RouteOptions>(routeOptions =>
{
    routeOptions.LowercaseUrls = true;
});


WebApplication app = builder.Build();
#endregion


#region configure the HTTP request pipeline
app.UseStaticFiles();
app.UseSwaggerSettings();
app.UseClientConfiguration();
//app.UseApiHttpLogging();
app.UseGlobalErrorHandler();
//app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.Run();


#endregion
