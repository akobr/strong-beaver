using StrongBeaver.Showroom.View.WebContent;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebViewPage : ContentPage
	{
		public WebViewPage(IWebContentDescription description)
		{
			InitializeComponent();
            ShowWebContent(description);
		}

        private void ShowWebContent(IWebContentDescription description)
        {
            Title = description.Title;
            WebViewContent.Source = description.Path;
        }
	}
}