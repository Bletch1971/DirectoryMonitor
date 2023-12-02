using DirectoryMonitor.App.Extensions;
using WPFSharp.Globalizer;

namespace DirectoryMonitor.ViewLib.Extensions;

public static class ApplicationExtensions
{
    public static string? DetectLanguage(this Application application)
    {
        ArgumentNullException.ThrowIfNull(application);

        var detectedLanguage = application.Resources.DetectLanguage();
        if (detectedLanguage is not null || application.MainWindow is null)
            return detectedLanguage;

        try
        {
            detectedLanguage = application.MainWindow.Resources.DetectLanguage();
            if (detectedLanguage is not null)
                return detectedLanguage;
        }
        catch (Exception)
        {
            //Trace.TraceWarning($"Failed to detect app style on main window.{Environment.NewLine}{ex}");
        }

        return detectedLanguage;
    }

    private static string? DetectLanguage(this ResourceDictionary resourceDictionary)
    {
        ArgumentNullException.ThrowIfNull(resourceDictionary);

        return DetectLanguageFromResources(resourceDictionary, out var detectedLanguage)
            ? detectedLanguage
            : null;
    }

    private static bool DetectLanguageFromResources(ResourceDictionary dict, out string? detectedLanguage)
    {
        using var enumerator = dict.MergedDictionaries.Reverse().GetEnumerator();
        while (enumerator.MoveNext())
        {
            var currentRd = enumerator.Current;
            if (currentRd is null)
                continue;

            if (currentRd.IsLanguageResourceDictionary())
            {
                detectedLanguage = currentRd.GetLanguage();
                return true;
            }

            if (DetectLanguageFromResources(currentRd, out detectedLanguage))
                return true;
        }

        detectedLanguage = null;
        return false;
    }

    private static string GetLanguage(this ResourceDictionary dict) =>
        dict is GlobalizationResourceDictionary grd
            ? grd.Name
            : string.Empty;

    private static bool IsLanguageResourceDictionary([NotNull]this ResourceDictionary dict) =>
        dict is GlobalizationResourceDictionary;

    public static object? TryFindResource(
        [NotNull]this Application application, [NotNull]string resourceKey, object fallbackValue) =>
        application.TryFindResource(resourceKey) ?? fallbackValue;

    public static string TryFindStringResource(
        [NotNull]this Application application, [NotNull]string resourceKey, [NotNull]string fallbackValue) =>
        (application.TryFindResource(resourceKey) as string ?? fallbackValue)
            .IReplace("\\r", "\r")
            .IReplace("\\n", "\n");

    public static void UpdateLanguage([NotNull]this Application application)
    {
        var currentLanguage = application.DetectLanguage();
        if (currentLanguage is null ||
            Thread.CurrentThread.CurrentCulture.Name.Equals(currentLanguage, StringComparison.OrdinalIgnoreCase))
            return;

        var currentCulture = new CultureInfo(currentLanguage);
        Thread.CurrentThread.CurrentCulture = currentCulture;
        Thread.CurrentThread.CurrentUICulture = currentCulture;
    }
}