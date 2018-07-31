using System.Collections.Generic;

namespace StrongBeaver.Core.Model
{
    public interface ISimpleStore<TKey, TItem> : IStore
    {
        int Count { get; }

        bool Contains(TKey key);

        TItem Get(TKey key);

        bool TryGet(TKey key, out TItem item);

        TItem Store(TItem item);

        void Delete(TKey key);

        IEnumerable<TKey> GetAllKeys();

        IEnumerable<TItem> GetAll();
    }
}
