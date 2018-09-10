using GalaSoft.MvvmLight.Ioc;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Dialog;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Showroom.Constants;
using StrongBeaver.Showroom.View;
using StrongBeaver.Showroom.ViewModel;
using Xamarin.Forms;

namespace StrongBeaver.Showroom
{
    public partial class App : Application
    {
        private const string VIEW_MODEL_LOCATOR_RESOURCE_KEY = "ViewModelLocator";

        public App()
        {
            InitializeComponent();
            SetViewModelLocator();

            MainPage = new MainPage();

            InitialiseNavigationAndDialogService();
        }

        public static IViewModelLocator ViewModelLocator { get; private set; }

        public static IPlatformModel PlatformModel { get; private set; }

        internal static void SetPlatformModel(IPlatformModel platformModel)
        {
            PlatformModel = platformModel;
        }

        private void SetViewModelLocator()
        {
            ViewModelLocator = (IViewModelLocator)Resources[VIEW_MODEL_LOCATOR_RESOURCE_KEY];
            SimpleIoc.Default.Register<IViewModelLocator>(() => { return ViewModelLocator; });
            ViewModel.ViewModelLocator.SetCurrentLocator(ViewModelLocator);
        }

        protected override void OnStart()
        {
            ViewModelLocator.Initialise();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void InitialiseNavigationAndDialogService()
        {
            InitialiseNavigationService();
            InitialiseDialogService();
        }

        private void InitialiseNavigationService()
        {
            Core.Activator.InitialiseNavigationService(((MainPage)MainPage).Detail.Navigation);

            INavigationService navigationService = ServiceProvider.Current.Get<INavigationService>();

            navigationService.Configure(ShowroomPageKeys.MAIN_PAGE, typeof(MainPage));
            navigationService.Configure(ShowroomPageKeys.WEB_VIEW_PAGE, typeof(WebViewPage));
            navigationService.Configure(ShowroomPageKeys.ARCHITECTORE_PAGE, typeof(ArchitecturePage));
            navigationService.Configure(ShowroomPageKeys.STORAGE_PAGE, typeof(StoragePage));
            navigationService.Configure(ShowroomPageKeys.NETWORK_PAGE, typeof(NetworkPage));
            navigationService.Configure(ShowroomPageKeys.DIALOG_PAGE, typeof(DialogPage));
            navigationService.Configure(ShowroomPageKeys.DEVICE_INFO_PAGE, typeof(DeviceInfoPage));
        }

        private void InitialiseDialogService()
        {
            Core.Activator.InitialiseDialogService(MainPage.Navigation);

            IDialogService dialogService = ServiceProvider.Current.Get<IDialogService>();

            dialogService.Configure(ShowroomDialogKeys.EXEMPLARY_DIALOG, typeof(ExemplaryDialog));
        }
    }
}
