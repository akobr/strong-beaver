using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using StrongBeaver.Core;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Core.ViewModel;
using StrongBeaver.Showroom.Constants;
using StrongBeaver.Showroom.View.WebContent;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StrongBeaver.Showroom.ViewModel
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private readonly INavigationService navigation;

        public MainViewModel()
        {
            navigation = ServiceLocator.Current.GetInstance<INavigationService>();
        }

        public ICommand NavigateToArchitecturePageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;

                // Using Provider as main Facade
                await Provider.Services.Get<INavigationService>().NavigateToAsync(ShowroomPageKeys.ARCHITECTORE_PAGE);
            });

        public ICommand NavigateToDialogPageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;

                // Using singleton service provider
                await ServiceProvider.Current.Get<INavigationService>().NavigateToAsync(ShowroomPageKeys.DIALOG_PAGE);
            });

        public ICommand NavigateToDeviceInfoPageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;

                // Using IoC container directly
                await ServiceLocator.Current.GetInstance<INavigationService>().NavigateToAsync(ShowroomPageKeys.DEVICE_INFO_PAGE);
            });

        public ICommand NavigateToStoragePageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;
                await navigation.NavigateToAsync(ShowroomPageKeys.STORAGE_PAGE);
            });

        public ICommand NavigateToNetworkPageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;
                await navigation.NavigateToAsync(ShowroomPageKeys.NETWORK_PAGE);
            });

        public ICommand NavigateToWebContentPageCommand
             => new RelayCommand<IWebContentDescription>(async (description) =>
             {
                 MasterPage.IsPresented = false;
                 await NavigateToLocalWebContentPageAsync(description);
             });

        private MasterDetailPage MasterPage => (MasterDetailPage)App.Current.MainPage;

        private Task NavigateToLocalWebContentPageAsync(IWebContentDescription description)
        {
            MasterPage.IsPresented = false;
            return navigation.NavigateToAsync(ShowroomPageKeys.WEB_VIEW_PAGE, description);
        }
    }
}
