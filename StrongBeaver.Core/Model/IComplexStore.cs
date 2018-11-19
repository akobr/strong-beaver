using StrongBeaver.Core.Lifetime;

namespace StrongBeaver.Core.Model
{
    public interface IComplexStore<TKey, TItem, TLifetimeManager> : IManagedStore<TKey, TItem, TLifetimeManager>
        where TLifetimeManager : class, ILifetimeManager
    {
        bool TryUpdate(TItem item);
    }
}
