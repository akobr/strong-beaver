﻿using System;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Navigation
{
    public interface INavigationService : IMessageBusService<INavigationMessage>
    {
        string CurrentPageKey { get; }

        bool CanGoBack();

        bool CanGoForward();

        Task GoBackAsync();

        Task GoForwardAsync();

        void Configure(string pageKey, Type pageType);

        Task NavigateToAsync(string pageKey);

        Task NavigateToAsync(string pageKey, object parameter);

        Task NavigateToAsync(Type pageType);

        Task NavigateToAsync(Type pageType, object parameter);

        Task<bool> NavigateToUriAsync(Uri requestedUri);

        Task<bool> CanNavigateToUriAsync(Uri requestedUri);
    }
}