namespace StrongBeaver.Core.Model
{
    public interface ISimpleStore<in TKey, TItem> : IStore
    {
        bool Contains(TKey key);

        TItem Get(TKey key);

        bool TryGet(TKey key, out TItem item);

        TItem Store(TItem item);

        void Delete(TKey key);
    }
}
