using WPFSharp.Globalizer;

namespace DirectoryMonitor.ViewLib.Converters;

public sealed class LocalizationConverter : MarkupExtension, IValueConverter
{
    private static LocalizationConverter? _localizationConverter;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is not string
            ? null
            : GlobalizedApplication.Instance.TryFindResource(value) ?? parameter;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        _localizationConverter ??= new LocalizationConverter();
}