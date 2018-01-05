using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArchitecturePage : ContentPage
	{
		public ArchitecturePage()
		{
			InitializeComponent();
            ScrollLater();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ScrollContainer.ScrollToAsync(300, 200, true);
        }

        private async void ScrollLater()
        {
            await Task.Delay(150);
            await ScrollContainer.ScrollToAsync(300, 200, false);
        }
    }
}