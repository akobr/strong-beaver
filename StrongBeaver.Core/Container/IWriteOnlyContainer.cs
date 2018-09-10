using System;

namespace StrongBeaver.Core.Container
{
    public interface IWriteOnlyContainer
    {
        bool IsRegistered<T>();

        bool IsRegistered<T>(string key);

        void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface;

        void Register<TClass>() where TClass : class;

        void Register<TClass>(Func<TClass> factory) where TClass : class;

        void Register<TClass>(Func<TClass> factory, string key) where TClass : class;

        void Unregister<TClass>() where TClass : class;

        void Unregister<TClass>(string key) where TClass : class;
    }
}