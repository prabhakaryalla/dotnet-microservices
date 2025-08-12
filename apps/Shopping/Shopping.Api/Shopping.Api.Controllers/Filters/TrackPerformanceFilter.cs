using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Shopping.Api.Contracts.Interfaces;
namespace Shopping.Api.Controllers.Filters;

public class TrackPerformanceFilter : IAsyncActionFilter
{
    private PerformanceTracker _tracker;
    private readonly IClientConfiguration _clientConfiguration;
    private readonly ILogger<TrackPerformanceFilter> _logger;


    public TrackPerformanceFilter(IClientConfiguration clientConfiguration, ILogger<TrackPerformanceFilter> logger)
    {
        _clientConfiguration = clientConfiguration;
        _logger = logger;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!(context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)) return;


        _tracker = new PerformanceTracker($"{actionDescriptor.ControllerName}/{actionDescriptor.ActionName}", _clientConfiguration, _logger);
        await next();
        _tracker?.Stop();
    }
}

