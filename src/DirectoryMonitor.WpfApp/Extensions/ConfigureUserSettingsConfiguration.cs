using DirectoryMonitor.App.Repositories;

namespace DirectoryMonitor.WpfApp.Extensions;

[ExcludeFromCodeCoverage]
internal static class ApplicationConfigurationRegistrationExtensions
{
    public static IConfigurationBuilder ConfigureUserSettingsConfiguration(
        this IConfigurationBuilder configBuilder, HostBuilderContext hostingContext)
    {
        FileUserSettingsRepository.CreateUserSettingsFile();

        configBuilder
            .AddJsonFile(FileUserSettingsRepository.UserSettingsFile, optional: true, reloadOnChange: true);
        return configBuilder;
    }
}