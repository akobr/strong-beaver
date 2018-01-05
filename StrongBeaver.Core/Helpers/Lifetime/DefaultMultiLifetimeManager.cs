using System;
using System.Collections.Generic;

namespace StrongBeaver.Core.Helpers.Lifetime
{
    public class DefaultMultiLifetimeManager<TKey, TLifetimeManager> : IMultiLifetimeManager<TKey, TLifetimeManager>
        where TLifetimeManager : class, ILifetimeManager
    {
        private readonly IFactory<TLifetimeManager> managerFactory;
        private readonly IDictionary<TKey, TLifetimeManager> managers;

        public DefaultMultiLifetimeManager(IFactory<TLifetimeManager> factory)
        {
            managerFactory = factory;
            managers = new Dictionary<TKey, TLifetimeManager>();
        }

        public DefaultMultiLifetimeManager(Func<TLifetimeManager> factory)
            : this(new DelegatedFactory<TLifetimeManager>(factory))
        {
            // No operation
        }

        public TLifetimeManager Create(TKey key)
        {
            if (managers.TryGetValue(key, out TLifetimeManager manager))
            {
                return manager;
            }

            TLifetimeManager newManager = managerFactory.Create();
            managers.Add(key, newManager);
            return newManager;
        }

        public void Destroy(TKey key)
        {
            managers.Remove(key);
        }

        public TLifetimeManager Get(TKey key)
        {
            if (!managers.TryGetValue(key, out TLifetimeManager manager))
            {
                return null;
            }

            return manager;
        }

        public bool IsAlive(TKey key)
        {
            if (!managers.TryGetValue(key, out TLifetimeManager manager))
            {
                return false;
            }

            return manager.IsAlive;
        }
    }
}
