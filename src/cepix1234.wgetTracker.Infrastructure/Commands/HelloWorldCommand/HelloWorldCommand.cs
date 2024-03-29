using cepix1234.wgetTracker.Core.Logging.Models;
using cepix1234.wgetTracker.Infrastructure.Commands.HelloWorldCommand.Settings;
using Spectre.Console.Cli;

namespace cepix1234.wgetTracker.Infrastructure.Commands.HelloWorldCommand;

public class HelloWorldCommand : AsyncCommand<HelloWorldCommandSettings>
{
    private readonly IConsoleLogger _consoleLogger;

    public HelloWorldCommand(IConsoleLogger consoleLogger)
    {
        _consoleLogger = consoleLogger;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, HelloWorldCommandSettings settings)
    {
        _consoleLogger.Log("Hello world!");
        return 0;
    }
}