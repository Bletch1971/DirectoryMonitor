using System.ComponentModel;
using ControlzEx.Theming;

namespace DirectoryMonitor.ViewLib.ObjectModels;

[ExcludeFromCodeCoverage]
public class AvailableThemes : SortableObservableCollection<ThemeBaseColor>
{
    private AvailableThemes()
    {
        ThemeManager.Current.ThemeChanged += ThemeChangedEventHandler;
    }

    private void ThemeChangedEventHandler(object? sender, ThemeChangedEventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedTheme)));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public static AvailableThemes Instance { get; private set; } = null!;

    public Theme? SelectedTheme => ThemeManager.Current.DetectTheme();

    public static void CreateInstance() =>
        Instance = new AvailableThemes();

    public void AddThemesFromThemeManager()
    {
        foreach (var baseColor in ThemeManager.Current.BaseColors.OrderBy(c => c))
        {
            var themeBaseColor = new ThemeBaseColor
            {
                DisplayName = baseColor,
            };
            themeBaseColor.AddThemesFromThemeManager();
            Add(themeBaseColor);
        }
    }
}