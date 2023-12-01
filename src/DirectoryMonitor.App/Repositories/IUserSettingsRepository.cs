using DirectoryMonitor.App.Settings;

namespace DirectoryMonitor.App.Repositories;

public interface IUserSettingsRepository
{
    Task<UserSettings> GetAsync();
    Task SaveAsync();
}