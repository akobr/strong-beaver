using System;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Showroom.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageDetail : ContentPage
    {
        private readonly INavigationService navigation;

        public MainPageDetail(INavigationService navigation)
        {
            this.navigation = navigation;

            InitializeComponent();
        }

        private async void HandleDialogTileTapped(object sender, EventArgs e)
        {
            await navigation.NavigateToAsync(ShowroomPageKeys.DIALOG_PAGE);
        }

        private async void HandleDeviceInfoTileTapped(object sender, EventArgs e)
        {
            await navigation.NavigateToAsync(ShowroomPageKeys.DEVICE_INFO_PAGE);
        }

        private async void HandleArchitectureTileTapped(object sender, EventArgs e)
        {
            await navigation.NavigateToAsync(ShowroomPageKeys.ARCHITECTORE_PAGE);
        }
    }
}