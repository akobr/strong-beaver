namespace StrongBeaver.Core.Model
{
    /// <summary>
    /// The complex store is extending the simple one.
    /// Initialisable, Updatable, and Disposable item.
    /// Lifetime of each item is hadled by reference counting.
    /// </summary>
    public interface IViewObjectStore<TKey, TItem> : ISimpleStore<TKey, TItem>
        where TItem : IViewObject<TItem>
    {
        bool TryUpdate(TItem item);

        void AddReference(TItem item);

        void RemoveReference(TItem item);
    }
}