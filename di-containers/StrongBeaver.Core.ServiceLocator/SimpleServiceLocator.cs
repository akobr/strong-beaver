using System;
using System.Collections.Generic;
using CommonServiceLocator;
using StrongBeaver.Core.Container;

namespace StrongBeaver.Core.ServiceLocator
{
    public class SimpleServiceLocator : IServiceLocator
    {
        private readonly SimpleIocContainer container;

        public SimpleServiceLocator(SimpleIocContainer container)
        {
            this.container = container;
        }

        public SimpleServiceLocator()
            : this(new SimpleIocContainer())
        {
            // no operation
        }

        public SimpleIocContainer Container => container;

        public object GetService(Type serviceType)
        {
            return container.GetService(serviceType);
        }

        public object GetInstance(Type serviceType)
        {
            return container.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return container.GetInstance(serviceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return container.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return container.GetInstance<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return container.GetAllInstances<TService>();
        }

        public static SimpleServiceLocator BuildAndRegister(SimpleIocContainer container)
        {
            SimpleServiceLocator locator = new SimpleServiceLocator(container);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => locator);
            return locator;
        }

        public static SimpleServiceLocator BuildAndRegister()
        {
            return BuildAndRegister(new SimpleIocContainer());
        }
    }
}
