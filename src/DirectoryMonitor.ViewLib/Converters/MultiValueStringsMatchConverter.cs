namespace DirectoryMonitor.ViewLib.Converters;

public sealed class MultiValueStringsMatchConverter : IMultiValueConverter
{
    public object Convert(object?[]? values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values is null || values.Length <= 1 || !targetType.IsAssignableFrom(typeof(bool)))
            return DependencyProperty.UnsetValue;

        return values
            .Select(v => v?.ToString() ?? "<#null#>")
            .Distinct()
            .ToArray()
            .Length == 1;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}