namespace DirectoryMonitor.ViewLib.ObjectModels;

/// <summary>
/// Represents a dynamic data collection that provides notifications
/// when items get added, removed, or when the whole list is refreshed and allows sorting.
/// </summary>
/// <typeparam name="T">The type of elements in the collection.</typeparam>
[ExcludeFromCodeCoverage]
public class SortableObservableCollection<T> : ObservableCollection<T>
{
    public SortableObservableCollection()
    {
    }

    public SortableObservableCollection(IEnumerable<T> collection)
        : base(collection)
    {
    }

    /// <summary>
    /// Sorts the items of the collection in ascending order according to a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">A function to extract a key from an item.</param>
    public SortableObservableCollection<T> Sort<TKey>(Func<T, TKey> keySelector)
    {
        InternalSort(Items.OrderBy(keySelector));
        return this;
    }

    /// <summary>
    /// Sorts the items of the collection in ascending order according to a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">A function to extract a key from an item.</param>
    /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
    public SortableObservableCollection<T> Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
    {
        InternalSort(Items.OrderBy(keySelector, comparer));
        return this;
    }

    /// <summary>
    /// Sorts the items of the collection in descending order according to a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">A function to extract a key from an item.</param>
    public SortableObservableCollection<T> SortDescending<TKey>(Func<T, TKey> keySelector)
    {
        InternalSort(Items.OrderByDescending(keySelector));
        return this;
    }

    /// <summary>
    /// Sorts the items of the collection in descending order according to a key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">A function to extract a key from an item.</param>
    /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
    public SortableObservableCollection<T> SortDescending<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer)
    {
        InternalSort(Items.OrderByDescending(keySelector, comparer));
        return this;
    }

    /// <summary>
    /// Moves the items of the collection so that their orders are the same as those of the items provided.
    /// </summary>
    /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
    private void InternalSort(IEnumerable<T> sortedItems)
    {
        var sortedItemsList = sortedItems.ToList();
        sortedItemsList
            .ForEach(item =>
                Move(IndexOf(item), sortedItemsList.IndexOf(item)));
    }

    public IList<T> ItemList => Items;
}