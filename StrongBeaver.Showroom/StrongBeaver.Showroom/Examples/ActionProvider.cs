using GalaSoft.MvvmLight.Ioc;
using StrongBeaver.Core;
using System;

namespace StrongBeaver.Showroom.Examples
{
    public interface IAction
    {
        // Implementation of action interface
    }

    public class ActionProvider : IProvider<IAction>
    {
        private SimpleIoc container;

        public ActionProvider(SimpleIoc container)
        {
            this.container = container;
        }

        public TInterface Get<TInterface>()
          where TInterface : IAction
        {
            // Always create a new instance of an action (command)
            return container.GetInstanceWithoutCaching<TInterface>();
        }

        public TInterface Get<TInterface>(string key)
          where TInterface : IAction
        {
            // Always create a new instance of an action (command)
            return container.GetInstanceWithoutCaching<TInterface>(key);
        }

        public void Register<TInterface, TClass>()
          where TInterface : class, IAction
          where TClass : class, TInterface
        {
            container.Register<TInterface, TClass>();
        }

        public void Register<TInterface>(Func<TInterface> factory, string key)
            where TInterface : class, IAction
        {
            container.Register<TInterface>(factory, key);
        }

        public void Unregister<TInterface>()
          where TInterface : class, IAction
        {
            container.Unregister<TInterface>();
        }

        public void Unregister<TInterface>(TInterface item)
          where TInterface : class, IAction
        {
            container.Unregister<TInterface>(item);
        }

        public void Unregister<TInterface>(string key)
          where TInterface : class, IAction
        {
            container.Unregister<TInterface>(key);
        }
    }
}
