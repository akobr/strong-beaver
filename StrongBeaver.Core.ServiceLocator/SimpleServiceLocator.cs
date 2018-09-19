using System;
using System.Collections.Generic;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace StrongBeaver.Core.ServiceLocator
{
    public class SimpleServiceLocator : IServiceLocator
    {
        private readonly SimpleIoc container;

        public SimpleServiceLocator(SimpleIoc container)
        {
            this.container = container;
        }

        public SimpleServiceLocator()
            : this(new SimpleIoc())
        {
            // no operation
        }

        public SimpleIoc Container => container;

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

        public static SimpleServiceLocator BuildAndRegister(SimpleIoc container)
        {
            SimpleServiceLocator locator = new SimpleServiceLocator(container);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => locator);
            return locator;
        }

        public static SimpleServiceLocator BuildAndRegister()
        {
            return BuildAndRegister(new SimpleIoc());
        }
    }
}
