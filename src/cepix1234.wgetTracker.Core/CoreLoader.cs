using cepix1234.wgetTracker.Core.Logging;
using cepix1234.wgetTracker.Core.Logging.Models;
using Microsoft.Extensions.DependencyInjection;

namespace cepix1234.wgetTracker.Core;

public static class CoreLoader
{
    public static void Load(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
    }
}