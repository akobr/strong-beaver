using System;
using StrongBeaver.Core.Helpers;
using StrongBeaver.Core.Lifetime;

namespace StrongBeaver.Core.Model
{
    public class ManagedStore<TKey, TItem, TLifetimeManager> : SimpleStore<TKey, TItem>, IManagedStore<TKey, TItem, TLifetimeManager>
        where TLifetimeManager : class, ILifetimeManager
    {
        private readonly IMultiLifetimeManager<TKey, TLifetimeManager> lifetimes;


        public ManagedStore(IFactory<TKey, TItem> keyFactory, Func<TLifetimeManager> lifetimeFactory)
            : base(keyFactory)
        {
            lifetimes = new DefaultMultiLifetimeManager<TKey, TLifetimeManager>(lifetimeFactory);
        }

        public ManagedStore(Func<TItem, TKey> keyFactory, Func<TLifetimeManager> lifetimeFactory)
            : this(new DelegatedFactory<TKey, TItem>(keyFactory), lifetimeFactory)
        {
            // No operation
        }

        public TLifetimeManager GetLifetimeManager(TKey key)
        {
            TryGetLifetimeManager(key, out TLifetimeManager manager);
            return manager;
        }

        public bool TryGetLifetimeManager(TKey key, out TLifetimeManager manager)
        {
            return lifetimes.TryGet(key, out manager);
        }

        public override TItem Store(TItem item)
        {
            if (item == null)
            {
                return default(TItem);
            }

            TKey key = keyFactory.Create(item);

            if (!OnBeforeItemCreation(key, item, out TItem resultItem))
            {
                return resultItem;
            }

            OnItemCreation(item);
            CreateLifetimeManager(key);
            items[key] = item;
            return item;
        }

        public override void Delete(TKey key)
        {
            if (!TryGet(key, out TItem existingItem))
            {
                return;
            }

            RemoveItem(key, existingItem);
        }

        protected TLifetimeManager CreateLifetimeManager(TKey key)
        {
            return lifetimes.Create(key);
        }

        protected void DestroyLifetimeManager(TKey key)
        {
            lifetimes.Destroy(key);
        }

        protected void RemoveItem(TKey key, TItem item)
        {
            items.Remove(key);
            DestroyLifetimeManager(key);

            OnItemRemove(item);
        }

        /// <returns>True if item should be created; otherwise false.</returns>
        protected virtual bool OnBeforeItemCreation(TKey key, TItem item, out TItem resultItem)
        {
            // template method
            resultItem = default(TItem);
            return true;
        }

        protected virtual void OnItemCreation(TItem item)
        {
            // No operation ( template method )
        }

        protected virtual void OnItemRemove(TItem item)
        {
            // No operation ( template method )
        }
    }
}
