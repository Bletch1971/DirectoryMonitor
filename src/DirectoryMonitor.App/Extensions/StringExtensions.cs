namespace DirectoryMonitor.App.Extensions;

public static class StringExtensions
{
    public static bool IEquals(this string? value1, string? value2) =>
        string.Equals(value1, value2, StringComparison.OrdinalIgnoreCase);

    public static string IReplace([NotNull]this string str, string oldValue, string? newValue) =>
        str.Replace(oldValue, newValue, StringComparison.OrdinalIgnoreCase);

    public static string IReplaceCount([NotNull]this string str, int newValue) =>
        str.Replace("{count}", newValue.ToStringInvariant(), StringComparison.OrdinalIgnoreCase);

    public static string IReplaceCount([NotNull]this string str, string? newValue) =>
        str.Replace("{count}", newValue, StringComparison.OrdinalIgnoreCase);

    public static string IReplaceError([NotNull]this string str, string? newValue) =>
        str.Replace("{error}", newValue, StringComparison.OrdinalIgnoreCase);

    public static string ToStringInvariant(this bool value) =>
        value.ToString(CultureInfo.CurrentCulture);

    public static string ToStringInvariant(this decimal value) =>
        value.ToString(CultureInfo.CurrentCulture);

    public static string ToStringInvariant(this double value) =>
        value.ToString(CultureInfo.CurrentCulture);

    public static string ToStringInvariant(this float value) =>
        value.ToString(CultureInfo.CurrentCulture);

    public static string ToStringInvariant(this int value) =>
        value.ToString(CultureInfo.CurrentCulture);

    public static string ToStringInvariant(this long value) =>
        value.ToString(CultureInfo.CurrentCulture);

    public static string ToStringInvariant(this short value) =>
        value.ToString(CultureInfo.CurrentCulture);
}