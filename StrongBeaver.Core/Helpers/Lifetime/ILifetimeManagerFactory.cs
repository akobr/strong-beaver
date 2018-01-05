namespace StrongBeaver.Core.Helpers.Lifetime
{
    public interface ILifetimeManagerFactory<out TLifetimeManager> : IFactory<TLifetimeManager>
        where TLifetimeManager : ILifetimeManager
    {
        // No member
    }
}
