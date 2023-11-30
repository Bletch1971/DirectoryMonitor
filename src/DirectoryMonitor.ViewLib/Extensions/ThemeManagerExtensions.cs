using ControlzEx.Theming;
using DirectoryMonitor.ViewLib.ObjectModels;

namespace DirectoryMonitor.ViewLib.Extensions;

public static class ThemeManagerExtensions
{
    public static SortableObservableCollection<ThemeBaseColor> ToSortableObservableCollection(this ThemeManager? themeManager) =>
        themeManager is null
            ? new SortableObservableCollection<ThemeBaseColor>()
            : themeManager.BaseColors
                .Select(baseColor => new ThemeBaseColor
                {
                    DisplayName = baseColor,
                    ColorSchemes = ThemeManager.Current.ColorSchemes
                        .Select(colorScheme => new ThemeColorScheme
                        {
                            DisplayName = colorScheme,
                            Theme = ThemeManager.Current.GetTheme(baseColor, colorScheme)
                        })
                        .ToSortableObservableCollection()
                        .Sort(i => i.DisplayName)
                })
                .ToSortableObservableCollection()
                .Sort(i => i.DisplayName);
}