﻿using ControlzEx.Theming;
using DirectoryMonitor.ViewLib.ObjectModels;

namespace DirectoryMonitor.ViewLib.Converters;

public class ThemeMatchConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var themeName = value switch
        {
            ThemeColorScheme {Theme: not null} colorScheme => colorScheme.Theme.DisplayName,
            Theme theme => theme.DisplayName,
            string name => name,
            _ => string.Empty
        };
        if (string.IsNullOrWhiteSpace(themeName) || !targetType.IsAssignableFrom(typeof(bool)))
            return DependencyProperty.UnsetValue;

        var currentTheme = AvailableThemes.Instance.SelectedTheme;
        return currentTheme is not null &&
               currentTheme.DisplayName.Equals(themeName, StringComparison.OrdinalIgnoreCase);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}