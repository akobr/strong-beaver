﻿using StrongBeaver.Core.Helpers;
using System;
using StrongBeaver.Core.Lifetime;

namespace StrongBeaver.Core.Model
{
    public class ComplexStore<TKey, TItem> : ManagedStore<TKey, TItem, ReferenceCountLifetimeManager>, IComplexStore<TKey, TItem>
        where TItem : IComplexStoreItem<TItem>
    {
        public ComplexStore(IFactory<TKey, TItem> factory)
            : base(factory, () => new ReferenceCountLifetimeManager())
        {
            // No operation
        }

        public ComplexStore(Func<TItem, TKey> factory)
            : this(new DelegatedFactory<TKey, TItem>(factory))
        {
            // No operation
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
                Provider.LogDebugMessage($"The complex store '{GetType().Name}' is removing item with key '{key}', because has no reference.");
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