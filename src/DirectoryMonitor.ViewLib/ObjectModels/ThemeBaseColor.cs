namespace DirectoryMonitor.ViewLib.ObjectModels;

[ExcludeFromCodeCoverage]
public sealed class ThemeBaseColor
{
    public string DisplayName { get; init; } = string.Empty;
    public SortableObservableCollection<ThemeColorScheme> ColorSchemes { get; init; } = new();
}