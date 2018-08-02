using System;
using StrongBeaver.Core.Lifetime;
using StrongBeaver.Core.Strategies;

namespace StrongBeaver.Core.Model
{
    public class ComplexIndependentManagedStore<TKey, TItem, TLifetimeManager> : ManagedStore<TKey, TItem, TLifetimeManager>, IComplexIndependentManagedStore<TKey, TItem, TLifetimeManager>
        where TLifetimeManager : class, ILifetimeManager
    {
        private readonly IInitialisationStrategy<TItem> initialisation;
        private readonly IUpdateStrategy<TItem> update;
        private readonly IDisposableStrategy<TItem> dispose;

        public ComplexIndependentManagedStore(
            IFactory<TKey, TItem> keyFactory,
            Func<TLifetimeManager> lifetimeManager,
            IInitialisationStrategy<TItem> initialisation,
            IUpdateStrategy<TItem> update,
            IDisposableStrategy<TItem> dispose)
            : base(keyFactory, lifetimeManager)
        {
            this.initialisation = initialisation;
            this.update = update;
            this.dispose = dispose;
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

        protected override bool OnBeforeItemCreation(TKey key, TItem item, out TItem resultItem)
        {
            if (!TryGet(key, out TItem existingItem))
            {
                resultItem = item;
                return true;
            }

            update?.UpdateItem(existingItem, item);
            resultItem = existingItem;
            return false;
        }

        protected override void OnItemCreation(TItem item)
        {
            initialisation?.InitialiseItem(item);
        }

        protected override void OnItemRemove(TItem item)
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

            base.OnCleanup();
        }

        protected override void OnDispose(bool disposing)
        {
            OnCleanup();
            base.OnDispose(disposing);
        }
    }
}
