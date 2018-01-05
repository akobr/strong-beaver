using System;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Navigation
{
    public interface INavigationService : IService, GalaSoft.MvvmLight.Views.INavigationService, IMessageBusService<INavigationMessage>
    {
        bool CanGoBack();

        bool CanGoForward();

        Task GoBackAsync();

        Task GoForwardAsync();

        void Configure(string pageKey, Type pageType);

        Task NavigateToAsync(string pageKey);

        Task NavigateToAsync(string pageKey, object parameter);

        Task NavigateToAsync(Type pageType);

        Task NavigateToAsync(Type pageType, object parameter);
    }
}