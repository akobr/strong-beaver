using System;

namespace StrongBeaver.Core
{
    public interface IProvider<in TProvidedItem>
        where TProvidedItem : class
    {
        void Register<TInterface, TClass>()
            where TInterface : class, TProvidedItem
            where TClass : class, TInterface;

        void Register<TInterface>(Func<TInterface> factory, string key)
            where TInterface : class, TProvidedItem;

        void Unregister<TInterface>()
            where TInterface : class, TProvidedItem;

        void Unregister<TInterface>(TInterface item)
            where TInterface : class, TProvidedItem;

        void Unregister<TInterface>(string key)
            where TInterface : class, TProvidedItem;

        TInterface Get<TInterface>()
            where TInterface : TProvidedItem;

        TInterface Get<TInterface>(string key)
            where TInterface : TProvidedItem;
    }
}
