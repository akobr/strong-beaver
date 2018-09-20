using System.Globalization;
using Foundation;
using GalaSoft.MvvmLight.Threading;
using StrongBeaver.Showroom.iOS.Platform;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Core.Services.Network.Http;
using StrongBeaver.Core.Services.Network.Rest;
using StrongBeaver.Core.Services.Reflection;
using StrongBeaver.Core.Services.Serialisation;
using StrongBeaver.Core.Services.Serialisation.Json;
using UIKit;
using StrongBeaver.Core.ViewModel;
using StrongBeaver.Platform;
using StrongBeaver.Services.Connectivity;
using StrongBeaver.Services.Dialog;
using StrongBeaver.Services.Geolocation;
using StrongBeaver.Services.Localisation;
using StrongBeaver.Services.Permissions;
using StrongBeaver.Services.Storage.Data;
using StrongBeaver.Services.Storage.Json;
using StrongBeaver.Services.Storage.KeyValues;
using StrongBeaver.Showroom.Constants;
using StrongBeaver.Showroom.iOS.Services.DataStorage;
using StrongBeaver.Showroom.Model;
using StrongBeaver.Showroom.Services.DataStorage;
using StrongBeaver.Showroom.View;
using Xamarin.Forms;
using StrongBeaver.Showroom.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using StrongBeaver.Core.Messaging;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Startup;
using StrongBeaver.Showroom.View.WebContent;

namespace StrongBeaver.Showroom.iOS
{
    public class Activator : BaseActivator
    {
        private readonly SimpleIoc container;
        private readonly UIApplication application;

        public Activator(UIApplication application)
        {
            container = new SimpleIoc();
            this.application = application;
        }

        public App Application { get; private set; }

        public override void Register()
        {
#if DEBUG
            // Debug logging
            container.Register<ILogService, DebugLogService>();
#else
            // Empty logging
            container.Register<ILogService, EmptyLogService>();
#endif
            // Platform registration
            container.Register<IPlatformFactory>(() => new IosPlatformFactory(() => new ShowroomIosApplicationInfo()));
            container.Register<IPlatformProvider, PlatformProvider>();

            // Instance creation
            container.Register<IInstanceCreationService, InstanceCreationService>();

            // Message bus
            //container.Register<IMessageBus, MessageBus>(); // Global message bus
            container.Register<IServiceMessageBus, ServiceMessageBus>();
            container.Register<IViewModelMessageBus, ViewModelMessageBus>();

            // JSON serialization
            container.Register<IJsonSerialisationService, JsonSerialisationService>();
            container.Register<ISerialisationService>(container.GetInstance<IJsonSerialisationService>);

            // Networking (HTTP and REST)
            container.Register<IHttpService, HttpService>();
            container.Register<IRestService, RestService>();

            // KeyValue pair storage
            container.Register<IKeyValuesStorageSyncService, XamarinApplicationKeyValuesStorageService>();

            // JSON storage
            container.Register<IJsonStorageService, XamarinApplicationJsonStorageService>();

            // SQLite persistent storage
            container.Register<ISqlContextFactory, IosSqLiteContextFactory>();
            container.Register<IDataBuilderFactory, ExamplaryDataBuilderFactory>();
            container.Register<IDataStorageService, SqLiteDataStorageService>();

            // Xamarin localisation
            container.Register<ILocalisationService, IosLocalisationService>();

            // Xamarin Permissions
            container.Register<IPermissionsService, XamarinPermissionsPluginService>();

            // Connectivity
            container.Register<IConnectivityService, XamarinConnectivityService>();

            // Geolocation
            container.Register<IGeolocationService, XamarinGeolocationService>();

            // Navigation
            container.Register<INavigationService, XamarinNavigationService>();

            // Dialogs
            container.Register<IDialogService, XamarinDialogService>();
            container.Register<IPlatformSpecificDialogService, IosSpecificDialogService>();

            // Examplary store
            container.Register<ExemplaryStore>();

            // View models
            container.Register<IViewModelProvider, ViewModelProvider>();
            container.Register<IEnvironmentViewModel, EnvironmentViewModel>();
            container.Register<IMainViewModel, MainViewModel>();
            container.Register<ExemplaryViewModel>();

            // Pages
            container.Register<MainPage>();
            container.Register<WebViewPage>();
            container.Register<ArchitecturePage>();
            container.Register<StoragePage>();
            container.Register<NetworkPage>();
            container.Register<DialogPage>();
            container.Register<DeviceInfoPage>();

            // Dialogues
            container.Register<ExemplaryDialog>();
        }

        public override void Resolve()
        {
            Initialise();

            ILogService logging = container.GetInstance<ILogService>();
            Application = new App(logging);

            MainPage mainPage = container.GetInstanceWithoutCaching<MainPage>();
            Application.MainPage = mainPage;
            InitialiseNavigationService(mainPage.Navigation);
            InitialiseDialogService(mainPage.Navigation);

            App.ViewModelLocator.Startup(logging, container.GetInstance<IViewModelProvider>());
        }

        private void Initialise()
        {
            // Global initilisations
            global::Xamarin.Forms.Forms.Init();
            DispatcherHelper.Initialize(application);

            // Local initilisations
            InitialiseInstanceCreation();
            InitialiseCulture();
            InitialiseLocalDatabase();
            InitiliaseLocalWebContentDescriptor();
        }

        private void InitiliaseLocalWebContentDescriptor()
        {
            LocalWebContentDescription.SetStaticPathPrefix(NSBundle.MainBundle.BundlePath);
        }

        private void InitialiseInstanceCreation()
        {
            ((InstanceCreationService)container.GetInstance<IInstanceCreationService>()).SetComplexCreationFactory(container.GetInstance);
        }

        private void InitialiseLocalDatabase()
        {
            IDataStorageService dataStorage = container.GetInstance<IDataStorageService>();

            using (IDataBuilder modelBuilder = dataStorage.GetBuilder())
            {
                modelBuilder.BuildStorageAsync();
            }
        }

        /// <summary>
        /// Initlialises Xamarin Navigation service.
        /// Should be called after <c>Initialise()</c>.
        /// </summary>
        private void InitialiseNavigationService(INavigation navigation)
        {
            INavigationService navigationService = container.GetInstance<INavigationService>();
            ((XamarinNavigationService)navigationService).Initialise(navigation);

            navigationService.Configure(ShowroomPageKeys.MAIN_PAGE, typeof(MainPage));
            navigationService.Configure(ShowroomPageKeys.WEB_VIEW_PAGE, typeof(WebViewPage));
            navigationService.Configure(ShowroomPageKeys.ARCHITECTORE_PAGE, typeof(ArchitecturePage));
            navigationService.Configure(ShowroomPageKeys.STORAGE_PAGE, typeof(StoragePage));
            navigationService.Configure(ShowroomPageKeys.NETWORK_PAGE, typeof(NetworkPage));
            navigationService.Configure(ShowroomPageKeys.DIALOG_PAGE, typeof(DialogPage));
            navigationService.Configure(ShowroomPageKeys.DEVICE_INFO_PAGE, typeof(DeviceInfoPage));
        }

        /// <summary>
        /// Initlialises Xamarin Dialog service.
        /// Should be called after <c>Initialise()</c>.
        /// </summary>
        private void InitialiseDialogService(INavigation navigation)
        {
            IDialogService dialogService = container.GetInstance<IDialogService>();
            ((XamarinDialogService)dialogService).Initialise(navigation);

            dialogService.Configure(ShowroomDialogKeys.EXEMPLARY_DIALOG, typeof(ExemplaryDialog));
        }

        private void InitialiseCulture()
        {
            IPlatformProvider platformProvider = container.GetInstance<IPlatformProvider>();

            if (platformProvider.PlatformInfo.PlatformType != PlatformTypeEnum.AppleIOS
                || platformProvider.PlatformInfo.PlatformType != PlatformTypeEnum.Android)
            {
                return;
            }

            ILocalisationService localise = container.GetInstance<ILocalisationService>();
            localise.SetCulture(localise.GetCurrentCultureInfo());
        }

        /// <summary>
        /// Initialises a specific .Net Resource file with localised strings to application culture through localisation service.
        /// </summary>
        private void InitialiseLocalisedResourceFileWithCulture(out CultureInfo targetCultureInResourceFile)
        {
            ILocalisationService localise = container.GetInstance<ILocalisationService>();
            targetCultureInResourceFile = localise.GetCurrentCultureInfo();
        }
    }
}
