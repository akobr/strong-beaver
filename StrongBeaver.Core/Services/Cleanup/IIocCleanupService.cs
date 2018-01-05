namespace StrongBeaver.Core.Services.Cleanup
{
    public interface IIocCleanupService : IService
    {
        void RemoveInstance<TClass>(TClass instance)
            where TClass : class;

        void RemoveInstance<TClass>(string key)
            where TClass : class;

        void Unregister<TInterface>()
            where TInterface : class;
    }
}
