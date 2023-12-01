using DirectoryMonitor.App.Settings;

namespace DirectoryMonitor.App.Repositories;

[ExcludeFromCodeCoverage]
public sealed class FileUserSettingsRepository : IUserSettingsRepository
{
    private const string ApplicationAuthor = "Bletch1971";
    private const string ApplicationId = "d14d6145-266d-4389-b776-f2883c2bf6b7";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly IOptionsMonitor<UserSettings> _settings;

    public FileUserSettingsRepository(
        IOptionsMonitor<UserSettings> settings)
    {
        _settings = settings;
    }

    public static string UserSettingsFile =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ApplicationAuthor,
            ApplicationId,
            "userSettings.json");

    public static void CreateUserSettingsFile()
    {
        if (File.Exists(UserSettingsFile))
            return;

        var settingsFolder = Path.GetDirectoryName(UserSettingsFile)!;
        if (!Directory.Exists(settingsFolder))
            Directory.CreateDirectory(settingsFolder);

        var userSettings = new
        {
            UserSettings = new UserSettings()
        };
        var userSettingsJson = JsonSerializer.Serialize(userSettings, JsonSerializerOptions);
        File.WriteAllText(UserSettingsFile, userSettingsJson);
    }

    public async Task<UserSettings> GetAsync() =>
        await ValueTask.FromResult(_settings.CurrentValue);

    public async Task SaveAsync()
    {
        var settingsFolder = Path.GetDirectoryName(UserSettingsFile)!;
        if (!Directory.Exists(settingsFolder))
            Directory.CreateDirectory(settingsFolder);

        var userSettings = new
        {
            UserSettings = _settings.CurrentValue
        };
        var userSettingsJson = JsonSerializer.Serialize(userSettings, JsonSerializerOptions);
        await File.WriteAllTextAsync(UserSettingsFile, userSettingsJson);
    }
}