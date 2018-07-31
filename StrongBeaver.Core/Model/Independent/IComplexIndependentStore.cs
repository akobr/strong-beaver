namespace StrongBeaver.Core.Model.Independent
{
    /// <summary>
    /// The complex store is extending the simple one.
    /// Lifetime of each item is hadled by reference counting.
    /// </summary>
    public interface IComplexIndependentStore<TKey, TItem> : ISimpleStore<TKey, TItem>
    {
        bool TryUpdate(TItem item);

        void AddReference(TItem item);

        void RemoveReference(TItem item);
    }
}
