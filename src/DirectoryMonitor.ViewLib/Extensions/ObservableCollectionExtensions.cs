using DirectoryMonitor.ViewLib.ObjectModels;

namespace DirectoryMonitor.ViewLib.Extensions;

public static class ObservableCollectionExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source) =>
        new(source);

    public static SortableObservableCollection<T> ToSortableObservableCollection<T>(this IEnumerable<T> source) =>
        new(source);

    public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> source) =>
        new(source.ToObservableCollection());
}