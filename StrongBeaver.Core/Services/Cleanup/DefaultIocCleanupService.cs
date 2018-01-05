using GalaSoft.MvvmLight.Ioc;

namespace StrongBeaver.Core.Services.Cleanup
{
    public class DefaultIocCleanupService : BaseService, IIocCleanupService
    {
        private readonly ISimpleIoc container;

        public DefaultIocCleanupService(ISimpleIoc container)
        {
            this.container = container;
        }

        public void RemoveInstance<TClass>(TClass instance)
            where TClass : class
        {
            container.Unregister(instance);
        }

        public void RemoveInstance<TClass>(string key)
            where TClass : class
        {
            container.Unregister(key);
        }

        public void Unregister<TInterface>()
            where TInterface : class
        {
            container.Unregister<TInterface>();
        }
    }
}
