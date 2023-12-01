using System.ComponentModel;
using ControlzEx.Theming;

namespace DirectoryMonitor.ViewLib.ObjectModels;

[ExcludeFromCodeCoverage]
public sealed class ThemeBaseColor : SortableObservableCollection<ThemeColorScheme>
{
    public ThemeBaseColor()
    {
        ThemeManager.Current.ThemeChanged += ThemeChangedEventHandler;
    }

    private void ThemeChangedEventHandler(object? sender, ThemeChangedEventArgs e)
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(DisplayName)));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public string DisplayName { get; init; } = string.Empty;

    public void AddThemesFromThemeManager()
    {
        foreach (var colorScheme in ThemeManager.Current.ColorSchemes.OrderBy(c => c))
        {
            Add(new ThemeColorScheme
            {
                DisplayName = colorScheme,
                Theme = ThemeManager.Current.GetTheme(DisplayName, colorScheme)
            });
        }
    }

    public override string ToString() => DisplayName;
}