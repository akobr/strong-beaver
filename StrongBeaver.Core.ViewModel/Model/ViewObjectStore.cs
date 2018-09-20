using System;
using StrongBeaver.Core.Lifetime;

namespace StrongBeaver.Core.Model
{
    public class ViewObjectStore<TKey, TItem> : ManagedStore<TKey, TItem, ReferenceCountLifetimeManager>, IViewObjectStore<TKey, TItem>
        where TItem : IViewObject<TItem>
    {
        public ViewObjectStore(IFactory<TKey, TItem> factory)
            : base(factory, () => new ReferenceCountLifetimeManager())
        {
            // no operation
        }

        public ViewObjectStore(Func<TItem, TKey> factory)
            : this(new DelegatedFactory<TKey, TItem>(factory))
        {
            // no operation
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

        public void AddReference(TItem item)
        {
            TKey key = keyFactory.Create(item);
            GetLifetimeManager(key)?.IncreaseReferences();
        }

        public void RemoveReference(TItem item)
        {
            TKey key = keyFactory.Create(item);
            ReferenceCountLifetimeManager lifetime = GetLifetimeManager(key);

            if (lifetime == null)
            {
                return;
            }

            lifetime.DescreseReferences();

            if (!lifetime.IsAlive)
            {
                RemoveItem(key, item);
            }
        }

        protected override bool OnBeforeItemCreation(TKey key, TItem item, out TItem resultItem)
        {
            if (!TryGet(key, out TItem existingItem))
            {
                resultItem = item;
                return true;
            }

            GetLifetimeManager(key).IncreaseReferences();
            existingItem.Update(item);
            resultItem = existingItem;
            return false;
        }

        protected override void OnItemCreation(TItem item)
        {
            item.Initialise();
        }

        protected override void OnItemRemove(TItem item)
        {
            item.Dispose();
        }

        protected override void OnCleanup()
        {
            foreach (TItem item in items.Values)
            {
                item.Dispose();
            }

            base.OnCleanup();
        }

        protected override void OnDispose(bool disposing)
        {
            OnCleanup();
            base.OnDispose(disposing);
        }
    }
}