using cepix1234.WgetTracker.Core;
using cepix1234.WgetTracker.Core.BackgroundServices.FileCollectorWorker;
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
app.SetDefaultCommand<WgetStatusCommand>();
app.Configure(
    config =>
    {
        config.ValidateExamples();
        
        config.AddCommand<WgetStatusCommand>("wgetStatus")
            .WithAlias("ws")
            .WithDescription("Get Status of all wget output files.")
            .WithExample("wgetStatus");
    });


return await app.RunAsync(args);

void RegisterServices(IServiceCollection services)
{
    CoreLoader.Load(services);
    InfrastructureLoader.Load(services);
}