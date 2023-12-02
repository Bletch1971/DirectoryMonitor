using System.ComponentModel;
using ControlzEx.Theming;
using WPFSharp.Globalizer;

namespace DirectoryMonitor.ViewLib.ObjectModels;

[ExcludeFromCodeCoverage]
public class AvailableThemes : SortableObservableCollection<ThemeBaseColor>
{
    private AvailableThemes()
    {
        GlobalizedApplication.Instance.GlobalizationManager.ResourceDictionaryChangedEvent += ResourceDictionaryChangedEventHandler;
        ThemeManager.Current.ThemeChanged += ThemeChangedEventHandler;
    }

    private void ResourceDictionaryChangedEventHandler(object source, ResourceDictionaryChangedEventArgs e) =>
        NotifyThemeChanged();

    private void ThemeChangedEventHandler(object? sender, ThemeChangedEventArgs e) =>
        NotifyThemeChanged();

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

    public void NotifyThemeChanged()
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedTheme)));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}