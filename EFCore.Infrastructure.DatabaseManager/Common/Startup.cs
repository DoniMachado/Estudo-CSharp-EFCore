using EFCore.Infrastructure.DatabaseManager.Common.Consts;
using EFCore.Infrastructure.DatabaseManager.Common.Enum;
using EFCore.Infrastructure.DatabaseManager.Common.Interfaces;
using EFCore.Infrastructure.DatabaseManager.Scripts.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFCore.Application;
using EFCore.Infrastructure;
using Microsoft.Extensions.Logging;
using EFCore.Application.Common.Behaviors;
using MediatR;
using System.Reflection;

namespace EFCore.Infrastructure.DatabaseManager.Common;

public class Startup
{
    private const string LOCAL = "local";

    private readonly IServiceCollection _serviceCollection;
    private readonly IConfiguration _configuration;
    private readonly EnvironmentDumpAction _action;
    private readonly EnvironmentDumpManager _environment;    
    private readonly ScriptRunner _scriptRunner;
    private IServiceProvider _serviceProvider;

    public Startup(EnvironmentDumpManager environmentChoice, EnvironmentDumpAction actionChoice, string clientName = "")
    {
        if(string.IsNullOrEmpty(clientName))
            clientName = LOCAL;

        var environment = environmentChoice switch
        {
            EnvironmentDumpManager.Development => "development",
            EnvironmentDumpManager.Staging => "staging",
            EnvironmentDumpManager.Production => "production",
            _ => ""
        };

        _environment = environmentChoice;

        _serviceCollection = new ServiceCollection();

        var builder = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();
        _serviceCollection.AddSingleton<IConfiguration>(provider => _configuration);
        _scriptRunner = new ScriptRunner(_configuration);
        _action = actionChoice;

        ConsoleDraw.DrawEnvironment(environment is "" ? "local" : environment);
    }

    public async Task RunAsync()
    {
        if (_action is EnvironmentDumpAction.Update || _action is EnvironmentDumpAction.Reset)
        {
            ConsoleDraw.DrawFeedback("Running Database Action");

            if (_environment is EnvironmentDumpManager.Staging || _environment is EnvironmentDumpManager.Production)
            {
                _scriptRunner.Run(false);
            }
            else
            {
                _scriptRunner.Run(_action is EnvironmentDumpAction.Reset);
            }
        }

        RegisterServices();

        await RunDumps();

        ConsoleDraw.DrawFeedback(string.Empty);
    }
    private void RegisterServices()
    {
        _serviceCollection.AddLogging(config =>
        {
            config.AddDebug();
            config.AddConsole();
        });

        _serviceCollection.AddApplication();
        _serviceCollection.AddInfrastructure(_configuration);
        _serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).Assembly);
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        AddDumps(_serviceCollection);

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    private async Task RunDumps()
    {
        ConsoleDraw.DrawFeedback("Dumps: ");

        var dumps = GetClassDump().Select(x => _serviceProvider.GetService(x) as IDump).OrderBy(d => d.Order);
        foreach ( var dump in dumps )
        {
            Thread.Sleep(1000);
            await dump.DumpAsync();
        }
    }

    private static Type[] GetClassDump()
    {
        return (from asm in AppDomain.CurrentDomain.GetAssemblies()
                from type in asm.GetExportedTypes()
                where type.IsClass && type.Name.EndsWith("Dump")
                select type).ToArray();
    }

    private void AddDumps (IServiceCollection services)
    {
        foreach (var item in GetClassDump())
            services.AddScoped(item);
    }
}
