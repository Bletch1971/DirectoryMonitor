using DirectoryMonitor.ViewLib.Extensions;
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

        Application.Current.UpdateLanguage();

        var ci = new CultureInfo(languageName);
        return Application.Current.TryFindResource($"Language_{languageName}", ci.DisplayName);
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