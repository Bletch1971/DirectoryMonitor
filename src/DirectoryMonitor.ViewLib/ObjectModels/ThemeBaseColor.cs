namespace DirectoryMonitor.ViewLib.ObjectModels;

public class ThemeBaseColor
{
    public string DisplayName { get; set; } = string.Empty;
    public SortableObservableCollection<ThemeColorScheme> ColorSchemes { get; set; } = new(Enumerable.Empty<ThemeColorScheme>());
}