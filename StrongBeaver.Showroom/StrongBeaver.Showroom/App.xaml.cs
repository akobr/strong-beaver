using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Showroom.View;
using StrongBeaver.Showroom.ViewModel;
using Xamarin.Forms;

namespace StrongBeaver.Showroom
{
    public partial class App : Application
    {
        private const string VIEW_MODEL_LOCATOR_RESOURCE_KEY = "ViewModelLocator";

        public App(ILogService logService)
        {
            Logger = logService;
            InitializeComponent();
            ViewModelLocator = (ViewModelLocator)Resources[VIEW_MODEL_LOCATOR_RESOURCE_KEY];
        }

        public static ILogService Logger { get; private set; }

        public static ViewModelLocator ViewModelLocator { get; private set; }

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
    }
}
