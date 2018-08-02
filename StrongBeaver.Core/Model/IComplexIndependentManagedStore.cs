using StrongBeaver.Core.Lifetime;

namespace StrongBeaver.Core.Model
{
    public interface IComplexIndependentManagedStore<TKey, TItem, TLifetimeManager> : IManagedStore<TKey, TItem, TLifetimeManager>
        where TLifetimeManager : class, ILifetimeManager
    {
        bool TryUpdate(TItem item);
    }
}
