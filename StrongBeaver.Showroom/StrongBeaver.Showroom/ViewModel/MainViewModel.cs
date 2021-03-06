﻿using StrongBeaver.Core;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Navigation;
using StrongBeaver.Core.ViewModel;
using StrongBeaver.Showroom.Constants;
using StrongBeaver.Showroom.View.WebContent;
using System.Threading.Tasks;
using System.Windows.Input;
using StrongBeaver.Core.Commands;
using Xamarin.Forms;

namespace StrongBeaver.Showroom.ViewModel
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private readonly INavigationService navigation;

        public MainViewModel(INavigationService navigation)
        {
            this.navigation = navigation;
        }

        public ICommand NavigateToArchitecturePageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;
                await navigation.NavigateToAsync(ShowroomPageKeys.ARCHITECTORE_PAGE);
            });

        public ICommand NavigateToDialogPageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;
                await navigation.NavigateToAsync(ShowroomPageKeys.DIALOG_PAGE);
            });

        public ICommand NavigateToDeviceInfoPageCommand
            => new RelayCommand(async () =>
            {
                MasterPage.IsPresented = false;
                await navigation.NavigateToAsync(ShowroomPageKeys.DEVICE_INFO_PAGE);
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

        private MasterDetailPage MasterPage => (MasterDetailPage)Application.Current.MainPage;

        private Task NavigateToLocalWebContentPageAsync(IWebContentDescription description)
        {
            MasterPage.IsPresented = false;
            return navigation.NavigateToAsync(ShowroomPageKeys.WEB_VIEW_PAGE, description);
        }
    }
}
