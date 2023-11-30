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

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
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
            .ConfigureLogManager(context.Configuration)
            .ConfigureApplicationServices(context.Configuration);
    }

    private static void RunApplication(IServiceProvider serviceProvider)
    {
        var window = serviceProvider.GetService<Window>()
                     ?? throw new InvalidOperationException($"Unable to resolve service for startup window.");
        window.Show();
    }
}