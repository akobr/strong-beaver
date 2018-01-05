namespace StrongBeaver.Core.Helpers.Lifetime
{
    interface IMultiLifetimeManager<in TKey, out TLifetimeManager>
        where TLifetimeManager : ILifetimeManager
    {
        TLifetimeManager Create(TKey key);

        TLifetimeManager Get(TKey key);

        void Destroy(TKey key);

        bool IsAlive(TKey key);
    }
}
