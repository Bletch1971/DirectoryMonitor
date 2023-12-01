using ControlzEx.Theming;
using DirectoryMonitor.ViewLib.ObjectModels;

namespace DirectoryMonitor.ViewLib.Converters;

public class ThemeMatchConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var themeDisplayName = value switch
        {
            ThemeColorScheme {Theme: not null} colorScheme => colorScheme.Theme.DisplayName,
            ThemeColorScheme {Theme: null} => string.Empty,
            Theme theme => theme.DisplayName,
            string name => name,
            _ => string.Empty
        };
        if (string.IsNullOrWhiteSpace(themeDisplayName) || !targetType.IsAssignableFrom(typeof(bool)))
            return DependencyProperty.UnsetValue;

        var currentTheme = AvailableThemes.Instance.SelectedTheme;
        return currentTheme is not null &&
               currentTheme.DisplayName.Equals(themeDisplayName, StringComparison.OrdinalIgnoreCase);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}