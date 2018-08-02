using StrongBeaver.Core.Lifetime;

namespace StrongBeaver.Core.Model
{
    public interface IManagedStore<TKey, TItem, TLifetimeManager> : ISimpleStore<TKey, TItem>
        where TLifetimeManager : class, ILifetimeManager
    {
        TLifetimeManager GetLifetimeManager(TKey key);

        bool TryGetLifetimeManager(TKey key, out TLifetimeManager manager);
    }
}