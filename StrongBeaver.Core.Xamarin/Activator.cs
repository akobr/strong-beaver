using System.Globalization;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using StrongBeaver.Core.Container;
using StrongBeaver.Core.Model;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Cleanup;
using StrongBeaver.Core.Services.Dialog;
using StrongBeaver.Core.Services.Localisation;
using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Core.Services.Network.Http;
using StrongBeaver.Core.Services.Network.Rest;
using StrongBeaver.Core.Services.Reflection;
using StrongBeaver.Core.Services.Serialisation;
using StrongBeaver.Core.Services.Serialisation.Json;
using StrongBeaver.Core.Services.Storage.Json;
using StrongBeaver.Core.Services.Storage.KeyValues;
using StrongBeaver.Core.ViewModel;
using Xamarin.Forms;

namespace StrongBeaver.Core
{
    public static class Activator
    {
        /// <summary>
        /// Initlialises the Service Locator (IoC container) and application culture.
        /// Must be called after <c>InitialiseIoc()</c>.
        /// </summary>
        public static void Initialise(IServiceLocator serviceLocator)
        {
            // Register default IoC container
            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            // Set providers
            ServiceProvider.SetCurrentProvider(serviceLocator.GetInstance<IServiceProvider>());
            StoreProvider.SetCurrentProvider(serviceLocator.GetInstance<IStoreProvider>());
            ViewModelProvider.SetCurrentProvider(serviceLocator.GetInstance<IViewModelProvider>());

            // Initialize an application culture
            InitialiseCulture(serviceLocator);
        }

        /// <summary>
        /// Initlialises Xamarin Navigation service.
        /// Should be called after <c>Initialise()</c>.
        /// </summary>
        public static void InitialiseNavigationService(INavigation navigation)
        {
            XamarinNavigationService navigationService = ServiceLocator.Current.GetInstance<INavigationService>() as XamarinNavigationService;
            navigationService?.Initialise(navigation);
        }

        /// <summary>
        /// Initlialises Xamarin Dialog service.
        /// Should be called after <c>Initialise()</c>.
        /// </summary>
        public static void InitialiseDialogService(INavigation navigation)
        {
            XamarinDialogService dialogService = ServiceLocator.Current.GetInstance<IDialogService>() as XamarinDialogService;
            dialogService?.Initialise(navigation);
        }

        /// <summary>
        /// Initialises a specific .Net Resource file with localised strings to application culture through localisation service.
        /// </summary>
        /// <param name="targetCultureInResourceFile"></param>
        public static void InitialiseLocalisedResourceFileWithCulture(ref CultureInfo targetCultureInResourceFile)
        {
            InitialiseLocalisedResourceFileWithCulture(ServiceLocator.Current, ref targetCultureInResourceFile);
        }

        public static void InitialiseLocalisedResourceFileWithCulture(IServiceLocator serviceLocator, ref CultureInfo targetCultureInResourceFile)
        {
            ILocalisationService localise = serviceLocator.GetInstance<ILocalisationService>();
            targetCultureInResourceFile = localise.GetCurrentCultureInfo();
        }

        /// <summary>
        /// Initialises Inversion of Control (IoC) container with a set of default services:
        /// IoC container itself is registered, as well;
        /// DebugLogService (only in DEBUG build);
        /// DefaultIocCleanupService;
        /// DefaultInstanceCreationService;
        /// DefaultJsonSerializationService;
        /// DefaultHttpService;
        /// DefaultRestService;
        /// XamarinApplicationKeyValuesStorageService;
        /// XamarinApplicationJsonStorageService;
        /// XamarinNavigationService;
        /// XamarinDialogService;
        /// <c>Should be called before <see cref="Initialise(IContainer)"/></c>.
        /// </summary>
        public static void InitialiseIoc(IContainer container)
        {
#if DEBUG
            // Debug logging
            container.Register<ILogService, DebugLogService>();
#else
            // Empty logging
            container.Register<ILogService, EmptyLogService>();
#endif
            // Platform model
            container.Register<IPlatformModel, DefaultPlatformModel>();

            // IoC container and cleanup
            container.Register<IContainer>(() => container);
            container.Register<IReadOnlyContainer>(() => container);
            container.Register<IIocCleanupService, DefaultIocCleanupService>();

            // Providers (services, stores, view models)
            container.Register<IServiceProvider, ServiceProvider>();
            container.Register<IStoreProvider, StoreProvider>();
            container.Register<IViewModelProvider, ViewModelProvider>();

            // Instance creation
            container.Register<IInstanceCreationService, DefaultInstanceCreationService>();

            // JSON serialization
            container.Register<IJsonSerialisationService, DefaultJsonSerialisationService>();
            container.Register<ISerialisationService>(container.GetInstance<IJsonSerialisationService>);

            // Networking (HTTP and REST)
            container.Register<IHttpService, DefaultHttpService>();
            container.Register<IRestService, DefaultRestService>();

            // KeyValue pair storage
            container.Register<IKeyValuesStorageSyncService, XamarinApplicationKeyValuesStorageService>();

            // JSON storage
            container.Register<IJsonStorageService, XamarinApplicationJsonStorageService>();

            // Navigation
            container.Register<INavigationService, XamarinNavigationService>();

            // Dialogs
            container.Register<IDialogService, XamarinDialogService>();
        }

        private static void InitialiseCulture(IServiceLocator serviceLocator)
        {
            IPlatformInfo platform = serviceLocator.GetInstance<IPlatformInfo>();

            if (platform == null)
            {
                Provider.LogWarningMessage("PlatformInfo object is not registered in IoC container.");
                return;
            }

            if (platform.PlatformType != PlatformTypeEnum.AppleIOS
                || platform.PlatformType != PlatformTypeEnum.Android)
            {
                return;
            }

            ILocalisationService localise = serviceLocator.GetInstance<ILocalisationService>();

            if (localise == null)
            {
                Provider.LogWarningMessage("The culture of the application can't be properly set, because Localisation service doesn't exist.");
                return;
            }

            localise.SetCulture(localise.GetCurrentCultureInfo());
        }
    }
}
