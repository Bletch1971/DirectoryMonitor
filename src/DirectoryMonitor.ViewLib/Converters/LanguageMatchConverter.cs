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

        return CultureInfo.CurrentCulture.Name
            .Equals(languageName, StringComparison.OrdinalIgnoreCase);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}