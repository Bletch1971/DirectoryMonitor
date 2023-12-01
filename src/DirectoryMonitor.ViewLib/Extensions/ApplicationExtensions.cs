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

    private static bool IsLanguageResourceDictionary(this ResourceDictionary dict) =>
        dict is GlobalizationResourceDictionary;
}