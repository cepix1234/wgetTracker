using cepix1234.WgetTracker.Core.Logging.Models;
using cepix1234.WgetTracker.Core.Models.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker;

public class FileCollectorWorker(IConsoleLogger consoleLogger, IOptions<AppSettings> appSettings)
    : BackgroundService
{
    private readonly IConsoleLogger _consoleLogger = consoleLogger;
    private readonly AppSettings _appSettings = appSettings.Value;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // do stuff
            _consoleLogger.Log($"Doing stuff");

            await Task.Delay(TimeSpan.FromSeconds(_appSettings.FileCollectionTaskTimout), stoppingToken);
        }
    }

}