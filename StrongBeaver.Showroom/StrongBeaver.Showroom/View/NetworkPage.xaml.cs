using Newtonsoft.Json.Linq;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Dialog;
using StrongBeaver.Core.Services.Network.Http;
using StrongBeaver.Core.Services.Network.Rest;
using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NetworkPage : ContentPage
    {
        private readonly IHttpService httpService;
        private readonly IRestService restService;
        private readonly IDialogService dialogService;

        public NetworkPage()
        {
            httpService = ServiceProvider.Current.Get<IHttpService>();
            restService = ServiceProvider.Current.Get<IRestService>();
            dialogService = ServiceProvider.Current.Get<IDialogService>();

            InitializeComponent();
        }

        private async void HandleSendHttpRequestButtonClicked(object sender, EventArgs e)
        {
            string url = string.IsNullOrWhiteSpace(entryHttpUrl.Text) ? "http://www.seznam.cz" : entryHttpUrl.Text;

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                dialogService.ShowToast(new Toast("Invalid URL, change a input."));
                return;
            }

            try
            {
                buttonHttp.IsEnabled = false;
                buttonHttp.BackgroundColor = Color.FromHex("#EEEEEE");
                progressHttp.IsVisible = true;

                IHttpRequest request = new HttpRequest(url);
                using (IHttpResponse response = await httpService.SendRequestAsync(request))
                {
                    if (!response.IsSuccess)
                    {
                        editorHttpResponse.Text = BuildUnsuccessfulResult(response);
                    }
                    else
                    {
                        editorHttpResponse.Text = await response.GetContentAsStringAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                editorHttpResponse.Text = BuildExceptionResult(exception);
            }
            finally
            {
                buttonHttp.IsEnabled = true;
                buttonHttp.BackgroundColor = Color.FromHex("#01579B");
                progressHttp.IsVisible = false;
            }
        }

        private async void HandleSendRestRequestButtonClicked(object sender, EventArgs e)
        {
            string uri = string.IsNullOrWhiteSpace(entryRestUri.Text) ? "http://services.groupkt.com/country/get/iso3code/CZE" : entryRestUri.Text;

            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                dialogService.ShowToast(new Toast("Invalid URI, change a input."));
                return;
            }

            try
            {
                buttonRest.IsEnabled = false;
                buttonRest.BackgroundColor = Color.FromHex("#EEEEEE");
                progressRest.IsVisible = true;

                IRestRequest request = new RestRequest(uri);
                IRestResponse<JObject> response = await restService.SendRequestAsync<JObject>(request);

                if (!response.IsSuccess)
                {
                    editorRestResponse.Text = BuildUnsuccessfulResult(response);
                }
                else
                {
                    editorRestResponse.Text = response.Content.ToString(Newtonsoft.Json.Formatting.Indented);
                }
            }
            catch (Exception exception)
            {
                editorRestResponse.Text = BuildExceptionResult(exception);
            }
            finally
            {
                buttonRest.IsEnabled = true;
                buttonRest.BackgroundColor = Color.FromHex("#01579B");
                progressRest.IsVisible = false;
            }
        }

        private string BuildUnsuccessfulResult(IResponse response)
        {
            if (response.HasException)
            {
                return BuildExceptionResult(response.Exception);
            }

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(response.StatusCode.ToString());
            builder.Append(response.ReasonPhrase);
            return builder.ToString();
        }

        private string BuildExceptionResult(Exception exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(exception.GetType().Name);
            builder.Append(exception.Message);
            return builder.ToString();
        }
    }
}