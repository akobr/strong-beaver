using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public class SimpleReadonlyContainer : ISimpleContainer
    {
        private readonly ISimpleContainer container;

        public SimpleReadonlyContainer(ISimpleContainer container)
        {
            this.container = container;
        }

        public ServiceDescriptor this[int index]
        {
            get => container[index];
            set => ThrowReadOnlyException();
        }

        public int Count => container.Count;

        public bool IsReadOnly => true;

        public bool Contains(ServiceDescriptor item)
            => container.Contains(item);

        public bool ContainsCreated<TClass>()
            => container.ContainsCreated<TClass>();

        public bool ContainsCreated<TClass>(string key)
            => container.ContainsCreated<TClass>(key);

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
            => container.CopyTo(array, arrayIndex);

        public object GetService(Type serviceType)
            => container.GetService(serviceType);

        public int IndexOf(ServiceDescriptor item)
            => container.IndexOf(item);

        public bool IsRegistered<T>()
            => container.IsRegistered<T>();

        public bool IsRegistered<T>(string key)
            => container.IsRegistered<T>(key);

        public TService GetInstance<TService>()
            => container.GetInstance<TService>();

        public TService GetInstance<TService>(string key)
            => container.GetInstance<TService>(key);

        public object GetInstance(Type serviceType)
            => container.GetInstance(serviceType);

        public object GetInstance(Type serviceType, string key)
            => container.GetInstance(serviceType, key);

        public TService GetInstanceWithoutCaching<TService>()
             => container.GetInstanceWithoutCaching<TService>();

        public TService GetInstanceWithoutCaching<TService>(string key)
            => container.GetInstanceWithoutCaching<TService>(key);

        public object GetInstanceWithoutCaching(Type serviceType)
            => container.GetInstanceWithoutCaching(serviceType);

        public object GetInstanceWithoutCaching(Type serviceType, string key)
            => container.GetInstanceWithoutCaching(serviceType, key);

        public IEnumerable<object> GetAllInstances(Type serviceType)
            => container.GetAllInstances(serviceType);

        public IEnumerable<TService> GetAllInstances<TService>()
            => container.GetAllInstances<TService>();

        public IEnumerator<ServiceDescriptor> GetEnumerator()
            => container.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => container.GetEnumerator();

        public void Add(ServiceDescriptor item)
        {
            ThrowReadOnlyException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            ThrowReadOnlyException();
        }

        public void Clear()
        {
            ThrowReadOnlyException();
        }

        public void Register<TClass>()
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Register<TClass>(bool createInstanceImmediately)
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Register<TClass>(Func<TClass> factory)
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately)
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Register<TClass>(Func<TClass> factory, string key) where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Register<TClass>(Func<TClass> factory, string key, bool createInstanceImmediately) where TClass : class
        {
            ThrowReadOnlyException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            ThrowReadOnlyException();
            return false;
        }

        public void RemoveAt(int index)
        {
            ThrowReadOnlyException();
        }

        public void Reset()
        {
            ThrowReadOnlyException();
        }

        public void Unregister<TClass>()
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Unregister<TClass>(TClass instance)
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        public void Unregister<TClass>(string key)
            where TClass : class
        {
            ThrowReadOnlyException();
        }

        void ISimpleContainer.Register<TInterface, TClass>()
        {
            ThrowReadOnlyException();
        }

        void ISimpleContainer.Register<TInterface, TClass>(bool createInstanceImmediately)
        {
            ThrowReadOnlyException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ThrowReadOnlyException()
        {
            throw new InvalidOperationException("The container is read-only.");
        }
    }
}
