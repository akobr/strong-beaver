﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public interface ISimpleContainer : IServiceCollection, IServiceProvider
    {
        bool ContainsCreated<TClass>();

        bool ContainsCreated<TClass>(string key);

        bool IsRegistered<T>();

        bool IsRegistered<T>(string key);

        TService GetInstance<TService>();

        TService GetInstance<TService>(string key);

        object GetInstance(Type serviceType);

        object GetInstance(Type serviceType, string key);

        TService GetInstanceWithoutCaching<TService>();

        TService GetInstanceWithoutCaching<TService>(string key);

        object GetInstanceWithoutCaching(Type serviceType);

        object GetInstanceWithoutCaching(Type serviceType, string key);

        IEnumerable<object> GetAllInstances(Type serviceType);

        IEnumerable<TService> GetAllInstances<TService>();

        void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface;

        void Register<TInterface, TClass>(bool createInstanceImmediately)
            where TInterface : class
            where TClass : class, TInterface;

        void Register<TClass>()
            where TClass : class;

        void Register<TClass>(bool createInstanceImmediately)
            where TClass : class;

        void Register<TClass>(Func<TClass> factory)
            where TClass : class;

        void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately)
            where TClass : class;

        void Register<TClass>(Func<TClass> factory, string key)
            where TClass : class;

        void Register<TClass>(
            Func<TClass> factory,
            string key,
            bool createInstanceImmediately)
            where TClass : class;

        void Reset();

        void Unregister<TClass>()
            where TClass : class;

        void Unregister<TClass>(TClass instance)
            where TClass : class;

        void Unregister<TClass>(string key)
            where TClass : class;
    }
}