using ControlzEx.Theming;

namespace DirectoryMonitor.ViewLib.ObjectModels;

[ExcludeFromCodeCoverage]
public sealed class ThemeColorScheme
{
    public string DisplayName { get; init; } = string.Empty;
    public Theme? Theme { get; init; }
}