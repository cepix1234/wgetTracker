using System.Diagnostics;
using cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker;
using cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker.Models;
using cepix1234.WgetTracker.Core.Logging.Models;
using cepix1234.WgetTracker.Core.Models.Application;
using cepix1234.WgetTracker.Core.WgetFile.Models;
using cepix1234.WgetTracker.Infrastructure.Commands.HelloWorldCommand.Settings;
using Microsoft.Extensions.Options;
using Spectre.Console.Cli;

namespace cepix1234.WgetTracker.Infrastructure.Commands.HelloWorldCommand;

public class WgetStatusCommand: AsyncCommand<WgetStatusCommandSettings>
{
    private readonly IConsoleLogger _consoleLogger;
    private readonly IFileCollectorService _fileCollectorService;
    private readonly AppSettings _appSettings;
    private int _previousNumberOfFiles;
    
    public WgetStatusCommand( IConsoleLogger consoleLogger, IFileCollectorService fileCollectorService, FileCollectorWorker filecollectorWorker, IOptions<AppSettings> appSettings)
    {
        _consoleLogger = consoleLogger;
        _fileCollectorService = fileCollectorService;
        filecollectorWorker.ExecuteAsync(new CancellationToken());
        _appSettings = appSettings.Value;
    }
    
    public override async Task<int> ExecuteAsync(CommandContext context, WgetStatusCommandSettings settings)
    {
        try
        {
            do
            {
                _consoleLogger.ResetCursor();
                List<IWgetFile> wgetFiles = _fileCollectorService.Files();
                if (wgetFiles.Count != _previousNumberOfFiles)
                {
                    _previousNumberOfFiles = wgetFiles.Count;
                    _consoleLogger.ClearConsole();
                }

                foreach (string directory in _appSettings.WgetDirectories)
                {
                    _consoleLogger.Log(string.Format("{0}{1}:", directory, Path.DirectorySeparatorChar));
                    foreach (IWgetFile wgetFile in wgetFiles.Where(wgetFile =>
                                 wgetFile.Direcory == directory && wgetFile.Exists()))
                    {
                        _consoleLogger.Log(wgetFile.ToString());
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            } while (true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return 1;
        }
    }
}