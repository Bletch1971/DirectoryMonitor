namespace DirectoryMonitor.ViewLib.Markup;

public sealed class IconExtension : MarkupExtension
{
    public string Source { get; set; } = "DirectoryMonitor";
    public string Path { get; set; } = string.Empty;
    public int Size { get; set; } = 16;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var decoder = BitmapDecoder.Create(
            new Uri($"pack://application:,,,/{Source};component/{Path}"),
            BitmapCreateOptions.DelayCreation,
            BitmapCacheOption.OnDemand);

        return decoder.Frames.SingleOrDefault(f => (int)f.Width == Size)
               ?? decoder.Frames.OrderBy(f => f.Width).First();
    }
}