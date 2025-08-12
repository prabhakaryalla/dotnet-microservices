using Microsoft.Extensions.Logging;
using Shopping.Api.Contracts.Interfaces;
using System.Diagnostics;

namespace Shopping.Api.Controllers.Filters;

public class PerformanceTracker
{
    private readonly string _apiOperation;
    private readonly Stopwatch _tracker;
    private readonly IClientConfiguration _clientConfiguration;
    private readonly ILogger _logger;


    public PerformanceTracker(string apiOperation, IClientConfiguration clientConfiguration, ILogger logger)
    {
        _apiOperation = apiOperation;
        _clientConfiguration = clientConfiguration;
        _tracker = new Stopwatch();
        _tracker.Start();
        _logger = logger;
    }


    public void Stop()
    {
        if (_tracker == null) return;
        _tracker.Stop();
        string loggingMessage = $"Tracking Process\"|ClientName=\"{_clientConfiguration.PartnerName}\"|ApiOperation=\"{_apiOperation}\"|RequestId=\"{_clientConfiguration.RequestId}\"|ProcessTime=\"{_tracker.ElapsedMilliseconds}";
        _logger.LogInformation(loggingMessage);
    }
}
