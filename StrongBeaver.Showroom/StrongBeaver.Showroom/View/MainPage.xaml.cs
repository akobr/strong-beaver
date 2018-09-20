using StrongBeaver.Core.Services.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        private readonly INavigationService navigation;

        public MainPage(INavigationService navigation)
        {
            this.navigation = navigation;

            InitializeComponent();
            Initialise();
        }

        private void Initialise()
        {
            Master = new MainPageMaster();
            Detail = new NavigationPage(new MainPageDetail(navigation));
        }
    }
}