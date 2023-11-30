using WPFSharp.Globalizer;

namespace DirectoryMonitor.ViewLib.Converters;

public sealed class LanguageNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var language = value as string;
        if (string.IsNullOrWhiteSpace(language))
            return null;

        var ci = new CultureInfo(language);
        var locConverter = new LocalizationConverter();
        return locConverter.Convert(language, typeof(string), ci.DisplayName, CultureInfo.CurrentCulture);
    }

    public object? ConvertBack(object? value, Type targetType, object?parameter, CultureInfo culture)
    {
        var language = value as string;
        if (string.IsNullOrWhiteSpace(language))
            return null;

        return AvailableLanguages.Instance.CultureInfoMap
            .FirstOrDefault(x => x.Value == language)
            .Key;
    }
}