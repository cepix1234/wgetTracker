using System.IO;
using cepix1234.WgetTracker.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using cepix1234.WgetTracker.Core.Models.Application;
using cepix1234.WgetTracker.Infrastructure;
using cepix1234.WgetTracker.Infrastructure.Commands.HelloWorldCommand;

var serviceCollection = new ServiceCollection()
    .AddLogging(configure =>
        configure
            .AddSimpleConsole(opts => { opts.TimestampFormat = "yyyy-MM-dd HH:mm:ss "; })
    );

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

serviceCollection.Configure<AppSettings>(configuration.GetSection("Settings"));
RegisterServices(serviceCollection);

using var registrar = new DependencyInjectionRegistrar(serviceCollection);
var app = new CommandApp(registrar);
app.SetDefaultCommand<HelloWorldCommand>();
app.Configure(
    config =>
    {
        config.ValidateExamples();
        
        config.AddCommand<HelloWorldCommand>("helloWorld")
            .WithAlias("hw")
            .WithDescription("Write hello world and if Argument passed check if it is in .*a.* format.")
            .WithExample("helloWorld", "[Argument]");
    });

return await app.RunAsync(args);

void RegisterServices(IServiceCollection services)
{
    CoreLoader.Load(services);
    InfrastructureLoader.Load(services);
}