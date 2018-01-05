using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Dialog;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExemplaryDialog : PopupPage
    {
        private readonly IDialogService dialogService;

        public ExemplaryDialog()
        {
            dialogService = ServiceProvider.Current.Get<IDialogService>();

            InitializeComponent();
        }

        protected override bool OnBackgroundClicked()
        {
            dialogService.ShowToast(new Toast("The custom dialog has been dismissed."));
            return base.OnBackgroundClicked();
        }

        protected override bool OnBackButtonPressed()
        {
            dialogService.ShowToast(new Toast("The custom dialog has been dismissed."));
            return base.OnBackButtonPressed();
        }

        private async void HandleFirstButtonClicked(object sender, System.EventArgs e)
        {
            dialogService.ShowToast(new Toast("The first button of custom dialog has been tapped."));
            await Navigation.PopPopupAsync();
        }

        private async void HandleSecondButtonClicked(object sender, System.EventArgs e)
        {
            dialogService.ShowToast(new Toast("The second button of custom dialog has been tapped."));
            await Navigation.PopPopupAsync();
        }
    }
}