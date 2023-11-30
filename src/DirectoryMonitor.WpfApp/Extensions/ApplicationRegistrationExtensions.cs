using DirectoryMonitor.WpfApp.Windows;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace DirectoryMonitor.WpfApp.Extensions;

[ExcludeFromCodeCoverage]
internal static class ApplicationRegistrationExtensions
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        // services
        //     .Configure<FileGameDataRepositorySettings>(configuration
        //         .GetSection(nameof(FileGameDataRepositorySettings)));
        // services.AddSingleton<IGameDataRepository, FileGameDataRepository>();
        //
        // services.AddSingleton<ICalculatorService, CalculatorService>();
        // services.AddSingleton<IGameDataService, GameDataService>();

        services.AddSingleton<Window, MainWindow>(); // defines the initial startup window
        services.AddSingleton<MainWindow>();
        return services;
    }

    public static IServiceCollection ConfigureLogManager(
        this IServiceCollection services, IConfiguration configuration)
    {
        LogManager.GlobalThreshold = configuration
                .GetValue("Logging:LogLevel:Default", LogLevel.None) switch
        {
            LogLevel.Trace => NLog.LogLevel.Trace,
            LogLevel.Debug => NLog.LogLevel.Debug,
            LogLevel.Information => NLog.LogLevel.Info,
            LogLevel.Warning => NLog.LogLevel.Warn,
            LogLevel.Error => NLog.LogLevel.Error,
            LogLevel.Critical => NLog.LogLevel.Fatal,
            LogLevel.None => NLog.LogLevel.Off,
            _ => NLog.LogLevel.Off
        };

        LogManager.Setup()
            .LoadConfigurationFromSection(configuration)
            .GetCurrentClassLogger();
        return services;
    }
}