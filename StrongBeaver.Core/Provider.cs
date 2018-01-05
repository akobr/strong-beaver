using System;
using GalaSoft.MvvmLight.Ioc;
using System.Diagnostics;
using StrongBeaver.Core.Services;
using Microsoft.Practices.ServiceLocation;
using StrongBeaver.Core.Model;
using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Core
{
    public class Provider<TProvidedItem> : IProvider<TProvidedItem>
        where TProvidedItem : class
    {
        private readonly ISimpleIoc container;

        public Provider(ISimpleIoc container)
        {
            this.container = container;
        }

        public TItem Get<TItem>()
            where TItem : TProvidedItem
        {
            return container.GetInstance<TItem>();
        }

        public TInterface Get<TInterface>(string key)
            where TInterface : TProvidedItem
        {
            return container.GetInstance<TInterface>(key);
        }

        public void Register<TInterface, TClass>()
            where TInterface : class, TProvidedItem
            where TClass : class, TInterface
        {
            container.Register<TInterface, TClass>();
        }

        public void Register<TInterface>(Func<TInterface> factory, string key)
            where TInterface : class, TProvidedItem
        {
            container.Register<TInterface>(factory, key);
        }

        public void Unregister<TInterface>()
            where TInterface : class, TProvidedItem
        {
            container.Unregister<TInterface>();
        }

        public void Unregister<TInterface>(TInterface item)
            where TInterface : class, TProvidedItem
        {
            container.Unregister<TInterface>(item);
        }

        public void Unregister<TInterface>(string key)
            where TInterface : class, TProvidedItem
        {
            container.Unregister<TInterface>(key);
        }
    }

    public static class Provider
    {
        public static IServiceLocator Locator => ServiceLocator.Current;

        public static Services.IServiceProvider Services => ServiceProvider.Current;

        public static IStoreProvider Stores => StoreProvider.Current;

        public static IViewModelProvider ViewModels => ViewModelProvider.Current;

        public static TItem Get<TItem>()
        {
            return ServiceLocator.Current.GetInstance<TItem>();
        }

        [Conditional("TRACE")]
        public static void LogTraceMessage(string message, params object[] args)
        {
            ServiceProvider.Current?.Logger?.Trace(message, args);
        }

        [Conditional("DEBUG")]
        public static void LogDebugMessage(string message, params object[] args)
        {
            ServiceProvider.Current?.Logger?.Debug(message, args);
        }

        public static void LogInfoMessage(string message, params object[] args)
        {
            ServiceProvider.Current?.Logger?.Info(message, args);
        }

        public static void LogWarningMessage(string message, params object[] args)
        {
            ServiceProvider.Current?.Logger?.Warn(message, args);
        }

        public static void LogErrorMessage(string message, params object[] args)
        {
            ServiceProvider.Current?.Logger?.Error(message, args);
        }
    }
}
