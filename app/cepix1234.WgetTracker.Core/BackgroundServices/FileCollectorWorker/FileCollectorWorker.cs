using cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker.Models;
using cepix1234.WgetTracker.Core.Models.Application;
using Microsoft.Extensions.Options;

namespace cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker;

public class FileCollectorWorker(IOptions<AppSettings> appSettings, IFileCollectorService fileCollectorService)
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            fileCollectorService.GetFiles();
            await Task.Delay(TimeSpan.FromSeconds(_appSettings.FileCollectionTaskTimout), stoppingToken);
        }
    }

}