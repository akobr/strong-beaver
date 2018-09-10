using System;
using CommonServiceLocator;
using StrongBeaver.Core;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Showroom.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        public MainPageDetail()
        {
            InitializeComponent();
        }

        private async void HandleDialogTileTapped(object sender, EventArgs e)
        {
            // Using singleton provider
            await ServiceProvider.Current.Get<INavigationService>().NavigateToAsync(ShowroomPageKeys.DIALOG_PAGE);
        }

        private async void HandleDeviceInfoTileTapped(object sender, EventArgs e)
        {
            // Using IoC container directly
            await ServiceLocator.Current.GetInstance<INavigationService>().NavigateToAsync(ShowroomPageKeys.DEVICE_INFO_PAGE);
        }

        private async void HandleArchitectureTileTapped(object sender, EventArgs e)
        {
            // Using Provider as main Facade
            await Provider.Services.Get<INavigationService>().NavigateToAsync(ShowroomPageKeys.ARCHITECTORE_PAGE);
        }
    }
}