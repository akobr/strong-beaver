using System;
using System.Collections.Generic;

namespace StrongBeaver.Core.Model
{
    public class SimpleStore<TKey, TItem> : BaseStore, ISimpleStore<TKey, TItem>
    {
        protected readonly IFactory<TKey, TItem> keyFactory;
        protected readonly IDictionary<TKey, TItem> items;

        public SimpleStore(IFactory<TKey, TItem> factory)
        {
            keyFactory = factory;
            items = new Dictionary<TKey, TItem>();
        }

        public SimpleStore(Func<TItem, TKey> factory)
            : this(new DelegatedFactory<TKey, TItem>(factory))
        {
            // No operation
        }

        public int Count => items.Count;

        public bool Contains(TKey key)
        {
            return items.ContainsKey(key);
        }

        public TItem Get(TKey key)
        {
            TryGet(key, out TItem item);
            return item;
        }

        public bool TryGet(TKey key, out TItem item)
        {
            return items.TryGetValue(key, out item);
        }

        public virtual TItem Store(TItem item)
        {
            if (item == null)
            {
                return default(TItem);
            }

            TKey key = keyFactory.Create(item);
            items[key] = item;
            return item;
        }

        public virtual void Delete(TKey key)
        {
            items.Remove(key);
        }

        public IEnumerable<TKey> GetAllKeys()
        {
            return items.Keys;
        }

        public IEnumerable<TItem> GetAll()
        {
            return items.Values;
        }

        protected override void OnCleanup()
        {
            items.Clear();
            base.OnCleanup();
        }
    }
}
