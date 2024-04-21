using cepix1234.WgetTracker.Core.Wrappers.Models;
using cepix1234.WgetTracker.Core.Wrappers;
using cepix1234.WgetTracker.Core.Logging;
using cepix1234.WgetTracker.Core.Logging.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader.Models;
using cepix1234.WgetTracker.Core.WgetOutputReader;
using Microsoft.Extensions.DependencyInjection;

namespace cepix1234.WgetTracker.Core;

public static class CoreLoader
{
    public static void Load(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConsoleLogger, ConsoleLogger>();
        serviceCollection.AddSingleton<IFileReader, FileReader>();
        serviceCollection.AddSingleton<IFileProvider, FileProvider>();
        serviceCollection.AddSingleton<IWgetOutputFileReader, WgetOutputFileReader>();
    }
}