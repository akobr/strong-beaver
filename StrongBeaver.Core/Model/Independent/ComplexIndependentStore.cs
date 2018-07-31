using StrongBeaver.Core.Helpers.Lifetime;
using StrongBeaver.Core.Strategies;

namespace StrongBeaver.Core.Model.Independent
{
    public class ComplexIndependentStore<TKey, TItem> : SimpleStore<TKey, TItem>, IComplexIndependentStore<TKey, TItem>
    {
        private readonly IMultiLifetimeManager<TKey, ReferenceCountLifetimeManager> lifetimes;
        private readonly IInitialisationStrategy<TItem> initialisation;
        private readonly IUpdateStrategy<TItem> update;
        private readonly IDisposableStrategy<TItem> dispose;

        public ComplexIndependentStore(
            IFactory<TKey, TItem> factory,
            IInitialisationStrategy<TItem> initialisation,
            IUpdateStrategy<TItem> update,
            IDisposableStrategy<TItem> dispose)
            : base(factory)
        {
            lifetimes = new DefaultMultiLifetimeManager<TKey, ReferenceCountLifetimeManager>(() => new ReferenceCountLifetimeManager());
            this.initialisation = initialisation;
            this.update = update;
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
                update?.UpdateItem(existingItem, item);
                return existingItem;
            }

            initialisation?.InitialiseItem(item);
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

            update.UpdateItem(existingItem, item);
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
            dispose?.DisposeItem(item);
        }

        protected override void OnCleanup()
        {
            if (dispose != null)
            {
                foreach (TItem item in items.Values)
                {
                    dispose.DisposeItem(item);
                }
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
