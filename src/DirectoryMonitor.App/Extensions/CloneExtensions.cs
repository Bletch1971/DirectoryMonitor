using System.Text.Json;
using System.Text.Json.Serialization;

namespace DirectoryMonitor.App.Extensions;

public static class CloneExtensions
{
    private static readonly JsonSerializerOptions DeserializeOptions = new()
    {
    };
    private static readonly JsonSerializerOptions SerializeOptions = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    public static T? Clone<T>(this T source) =>
        // Don't serialize a null object, simply return the default for that object
        source is null
            ? default
            : JsonSerializer
                .Deserialize<T>(JsonSerializer
                    .Serialize(source, SerializeOptions), DeserializeOptions);
}