using System;
using System.Collections.Generic;
using CommonServiceLocator;
using StrongBeaver.Core.Container;

namespace StrongBeaver.Core.ServiceLocator
{
    public class SimpleServiceLocator : IServiceLocator
    {
        private readonly SimpleContainer container;

        public SimpleServiceLocator(SimpleContainer container)
        {
            this.container = container;
        }

        public SimpleServiceLocator()
            : this(new SimpleContainer())
        {
            // no operation
        }

        public SimpleContainer Container => container;

        public object GetService(Type serviceType)
        {
            return container.GetInstance(serviceType);
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

        public static SimpleServiceLocator BuildAndRegister(SimpleContainer container)
        {
            SimpleServiceLocator locator = new SimpleServiceLocator(container);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => locator);
            return locator;
        }

        public static SimpleServiceLocator BuildAndRegister()
        {
            return BuildAndRegister(new SimpleContainer());
        }
    }
}
