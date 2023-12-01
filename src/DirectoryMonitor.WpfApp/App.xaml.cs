using DirectoryMonitor.ViewLib.ObjectModels;
using DirectoryMonitor.WpfApp.Extensions;

namespace DirectoryMonitor.WpfApp;

[ExcludeFromCodeCoverage]
public sealed partial class App
{
    private static IHost? _host;

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        _host = Host
            .CreateDefaultBuilder(e.Args)
            .ConfigureAppConfiguration(ConfigureAppConfiguration)
            .ConfigureServices(ConfigureServices)
            .Build();

        await _host.StartAsync();

        RunApplication(_host.Services);
    }

    private async void OnExit(object sender, ExitEventArgs e)
    {
        LogManager.Flush();
        LogManager.Shutdown();

        if (_host is null)
            return;

        using (_host)
            await _host.StopAsync(TimeSpan.FromSeconds(5));
    }

    public override void Init()
    {
        base.Init();

        AvailableThemes.CreateInstance();
        AvailableThemes.Instance.AddThemesFromThemeManager();
    }

    private void ConfigureAppConfiguration(HostBuilderContext hostingContext, IConfigurationBuilder appConfigBuilder)
    {
    }

    private static void ConfigureServices(HostBuilderContext hostingContext, IServiceCollection services)
    {
        services
            //.AddAutoMapper(typeof(MapProfile))
            .AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog();
            });

        services
            .ConfigureLogManager(hostingContext.Configuration)
            .ConfigureApplicationServices(hostingContext.Configuration);
    }

    private static void RunApplication(IServiceProvider serviceProvider)
    {
        var window = serviceProvider.GetService<Window>()
                     ?? throw new InvalidOperationException($"Unable to resolve service for startup window.");
        window.Show();
    }
}