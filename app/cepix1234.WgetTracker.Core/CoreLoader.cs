using cepix1234.WgetTracker.Core.Logging;
using cepix1234.WgetTracker.Core.Logging.Models;
using Microsoft.Extensions.DependencyInjection;

namespace cepix1234.WgetTracker.Core;

public static class CoreLoader
{
    public static void Load(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
    }
}