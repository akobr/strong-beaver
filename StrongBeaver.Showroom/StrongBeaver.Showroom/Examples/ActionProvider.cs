using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services.Reflection;

namespace StrongBeaver.Showroom.Examples
{
    public interface IAction
    {
        // Implementation of action interface
    }

    public class ActionProvider
    {
        private readonly InstanceCreationService objectCreation;
        private readonly Dictionary<Type, Type> registrations;

        public ActionProvider(InstanceCreationService objectCreation)
        {
            this.objectCreation = objectCreation;
            registrations = new Dictionary<Type, Type>();
        }

        public TInterface Get<TInterface>()
            where TInterface : class, IAction
        {
            Type requestedType = typeof(TInterface);

            if (!registrations.TryGetValue(requestedType, out Type responseType))
            {
                return null;
            }

            return objectCreation.CreateInstance(responseType) as TInterface;
        }

        public void Register<TInterface, TClass>()
            where TInterface : class, IAction
            where TClass : class, TInterface
        {
            registrations[typeof(TInterface)] = typeof(TClass);
        }

        public void Unregister<TInterface>()
            where TInterface : class, IAction
        {
            registrations.Remove(typeof(TInterface));
        }
    }
}