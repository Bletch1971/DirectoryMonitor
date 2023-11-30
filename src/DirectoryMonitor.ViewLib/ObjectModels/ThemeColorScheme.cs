using ControlzEx.Theming;

namespace DirectoryMonitor.ViewLib.ObjectModels;

public class ThemeColorScheme
{
    public string DisplayName { get; set; } = string.Empty;
    public Theme? Theme { get; set; }
}