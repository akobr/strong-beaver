using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using StrongBeaver.Services.Toast;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExemplaryDialog : PopupPage
    {
        private readonly IToastService toastService;

        public ExemplaryDialog(IToastService toastService)
        {
            this.toastService = toastService;

            InitializeComponent();
        }

        protected override bool OnBackgroundClicked()
        {
            toastService.ShowToast(new Toast("The custom dialog has been dismissed."));
            return base.OnBackgroundClicked();
        }

        protected override bool OnBackButtonPressed()
        {
            toastService.ShowToast(new Toast("The custom dialog has been dismissed."));
            return base.OnBackButtonPressed();
        }

        private async void HandleFirstButtonClicked(object sender, System.EventArgs e)
        {
            toastService.ShowToast(new Toast("The first button of custom dialog has been tapped."));
            await Navigation.PopPopupAsync();
        }

        private async void HandleSecondButtonClicked(object sender, System.EventArgs e)
        {
            toastService.ShowToast(new Toast("The second button of custom dialog has been tapped."));
            await Navigation.PopPopupAsync();
        }
    }
}