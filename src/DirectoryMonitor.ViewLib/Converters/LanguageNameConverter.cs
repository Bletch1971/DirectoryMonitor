using WPFSharp.Globalizer;

namespace DirectoryMonitor.ViewLib.Converters;

public sealed class LanguageNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var languageName = value switch
        {
            string name => name,
            _ => string.Empty
        };
        if (string.IsNullOrWhiteSpace(languageName) || !targetType.IsAssignableFrom(typeof(string)))
            return string.Empty;

        var ci = new CultureInfo(languageName);
        var converter = new LocalizationConverter();
        return converter.Convert(languageName, typeof(string), ci.DisplayName, CultureInfo.CurrentCulture);
    }

    public object? ConvertBack(object? value, Type targetType, object?parameter, CultureInfo culture)
    {
        var languageName = value switch
        {
            string name => name,
            _ => string.Empty
        };
        if (string.IsNullOrWhiteSpace(languageName) || !targetType.IsAssignableFrom(typeof(string)))
            return string.Empty;

        return AvailableLanguages.Instance.CultureInfoMap
            .FirstOrDefault(x => x.Value == languageName)
            .Key;
    }
}