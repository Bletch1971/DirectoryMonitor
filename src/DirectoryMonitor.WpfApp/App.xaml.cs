using ControlzEx.Theming;
using DirectoryMonitor.App.Settings;
using DirectoryMonitor.ViewLib.ObjectModels;
using DirectoryMonitor.WpfApp.Extensions;
using Microsoft.Extensions.Options;

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

        SetupApplication(_host.Services);
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

    private static void ConfigureAppConfiguration(
        HostBuilderContext hostingContext, IConfigurationBuilder configBuilder)
    {
        configBuilder
            .ConfigureUserSettingsConfiguration(hostingContext);
    }

    private static void ConfigureServices(
        HostBuilderContext hostingContext, IServiceCollection services)
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

    private static void SetupApplication(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetService<ILogger<App>>();
        var userSettings = serviceProvider.GetService<IOptions<UserSettings>>()?
            .Value;
        if (userSettings is null)
            return;

        try
        {
            if (!string.IsNullOrWhiteSpace(userSettings.Language))
                Instance.GlobalizationManager.SwitchLanguage(userSettings.Language, true);
        }
        catch (Exception ex)
        {
            // just output the exception message, and move on
            logger?.LogDebug("Error occurred while setting language: {Error}", ex.Message);
        }

        try
        {
            if (!string.IsNullOrWhiteSpace(userSettings.Theme))
                ThemeManager.Current.ChangeTheme(Instance, userSettings.Theme);
        }
        catch (Exception ex)
        {
            // just output the exception message, and move on
            logger?.LogDebug("Error occurred while setting theme: {Error}", ex.Message);
        }
    }
}