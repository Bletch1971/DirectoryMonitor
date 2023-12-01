using DirectoryMonitor.ViewLib.Extensions;

namespace DirectoryMonitor.ViewLib.Converters;

public class LanguageMatchConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var languageName = value switch
        {
            string name => name,
            _ => string.Empty
        };
        if (string.IsNullOrWhiteSpace(languageName) || !targetType.IsAssignableFrom(typeof(bool)))
            return DependencyProperty.UnsetValue;

        var currentLanguage = Application.Current.DetectLanguage();
        return currentLanguage is not null &&
               currentLanguage.Equals(languageName, StringComparison.OrdinalIgnoreCase);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}