using System.ComponentModel;
using cepix1234.WgetTracker.Infrastructure.Commands.HelloWorldCommand.Validation;
using Spectre.Console.Cli;

namespace cepix1234.WgetTracker.Infrastructure.Commands.HelloWorldCommand.Settings;

public class HelloWorldCommandSettings : CommandSettings
{
    [CommandArgument(0, "[Argument]")]
    [Description("Optional argument to validate must be in .*a.* format")]
    [HelloWorldCommandValidation]
    public string Argument { get; set; } = String.Empty;
}