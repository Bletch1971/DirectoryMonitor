using ControlzEx.Theming;
using DirectoryMonitor.ViewLib.Extensions;
using DirectoryMonitor.ViewLib.ObjectModels;

namespace DirectoryMonitor.ViewLib.Converters;

public class ThemeNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var themeName = value switch
        {
            ThemeColorScheme colorScheme => colorScheme.DisplayName,
            ThemeBaseColor baseColor => baseColor.DisplayName,
            Theme theme => theme.DisplayName,
            string name => name,
            _ => string.Empty
        };
        if (string.IsNullOrWhiteSpace(themeName) || !targetType.IsAssignableFrom(typeof(bool)))
            return DependencyProperty.UnsetValue;

        Application.Current.UpdateLanguage();

        return Application.Current.TryFindStringResource($"Theme_{themeName}", themeName);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}