using StrongBeaver.Core.Helpers;
using StrongBeaver.Core.Helpers.Lifetime;
using StrongBeaver.Core.Services;
using System;

namespace StrongBeaver.Core.Model
{
    public class ComplexStore<TKey, TItem> : SimpleStore<TKey, TItem>, IComplexStore<TKey, TItem>
        where TItem : IComplexStoreItem<TItem>
    {
        private readonly IMultiLifetimeManager<TKey, ReferenceCountLifetimeManager> lifetimes;

        public ComplexStore(IFactory<TKey, TItem> factory)
            : base(factory)
        {
            lifetimes = new DefaultMultiLifetimeManager<TKey, ReferenceCountLifetimeManager>(() => new ReferenceCountLifetimeManager());
        }

        public ComplexStore(Func<TItem, TKey> factory)
            : this(new DelegatedFactory<TKey, TItem>(factory))
        {
            // No operation
        }

        public override TItem Store(TItem item)
        {
            if (item == null)
            {
                return default(TItem);
            }

            TKey key = keyFactory.Create(item);

            if (TryGet(key, out TItem existingItem))
            {
                lifetimes.Get(key).IncreaseReferences();
                existingItem.Update(item);
                return existingItem;
            }

            item.Initialise();
            lifetimes.Create(key);
            items[key] = item;
            return item;
        }

        public bool TryUpdate(TItem item)
        {
            if (item == null)
            {
                return false;
            }

            TKey key = keyFactory.Create(item);

            if (!TryGet(key, out TItem existingItem))
            {
                return false;
            }

            existingItem.Update(item);
            return true;
        }

        public override void Delete(TKey key)
        {
            if (!TryGet(key, out TItem existingItem))
            {
                return;
            }

            RemoveItem(key, existingItem);
        }

        public void AddReference(TItem item)
        {
            TKey key = keyFactory.Create(item);
            lifetimes.Get(key)?.IncreaseReferences();
        }

        public void RemoveReference(TItem item)
        {
            TKey key = keyFactory.Create(item);
            ReferenceCountLifetimeManager lifetime = lifetimes.Get(key);

            if (lifetime == null)
            {
                return;
            }

            lifetime.DescreseReferences();

            if (!lifetime.IsAlive)
            {
                Provider.LogDebugMessage($"The complex store '{GetType().Name}' is removing item with key '{key}', because has no reference.");
                RemoveItem(key, item);
            }
        }

        protected virtual void OnItemRemove(TItem item)
        {
            item.Dispose();
        }

        protected override void OnCleanup()
        {
            foreach (TItem item in items.Values)
            {
                item.Dispose();
            }

            items.Clear();
            base.OnCleanup();
        }

        protected override void OnDispose(bool disposing)
        {
            OnCleanup();
            base.OnDispose(disposing);
        }

        private void RemoveItem(TKey key, TItem item)
        {
            items.Remove(key);
            lifetimes.Destroy(key);

            OnItemRemove(item);
        }
    }
}