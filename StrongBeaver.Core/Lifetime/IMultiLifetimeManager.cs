namespace StrongBeaver.Core.Lifetime
{
    interface IMultiLifetimeManager<in TKey, TLifetimeManager>
        where TLifetimeManager : ILifetimeManager
    {
        TLifetimeManager Create(TKey key);

        TLifetimeManager Get(TKey key);

        bool TryGet(TKey key, out TLifetimeManager manager);

        void Destroy(TKey key);

        bool IsAlive(TKey key);
    }
}
