﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StrongBeaver.Core.Services.Reflection;
using Xamarin.Forms;

namespace StrongBeaver.Core.Services.Navigation
{
    public class XamarinNavigationService : BaseService, INavigationService
    {
        private readonly IInstanceCreationService reflectionService;
        private readonly IDictionary<string, Type> registeredPages;

        private INavigation xamarinNavigation;
        private Stack<Page> forwardStack;

        public XamarinNavigationService(IInstanceCreationService reflectionService)
        {
            this.reflectionService = reflectionService;
            registeredPages = new Dictionary<string, Type>();
        }

        public bool SupportForwarding => forwardStack != null;

        [Obsolete("Don't use this property. Possible performance problems.")]
        public string CurrentPageKey
        {
            get
            {
                if (xamarinNavigation.NavigationStack.Count < 1)
                {
                    return string.Empty;
                }

                string typeFullName = xamarinNavigation.NavigationStack[0].GetType().FullName;
                string findedPageKey = registeredPages
                    .Where(pv => pv.Value.FullName == typeFullName)
                    .Select(pv => pv.Key).FirstOrDefault();

                return string.IsNullOrEmpty(findedPageKey)
                    ? typeFullName
                    : findedPageKey;
            }
        }

        public void Initialise(INavigation navigation, bool supportForwarding = false)
        {
            xamarinNavigation = navigation;

            if (!supportForwarding)
            {
                return;
            }

            forwardStack = new Stack<Page>();
        }

        public bool CanGoBack()
        {
            return xamarinNavigation.NavigationStack.Count > 1;
        }

        public bool CanGoForward()
        {
            return SupportForwarding && forwardStack.Count > 0;
        }

        public Task GoBackAsync()
        {
            if (!CanGoBack())
            {
                return Task.FromResult(0);
            }

            return SupportForwarding
                ? GoBackWithForwardingAsync()
                : xamarinNavigation.PopAsync();
        }

        public void GoBack()
        {
            GoBackAsync();
        }

        public Task GoForwardAsync()
        {
            if (!CanGoForward())
            {
                return Task.FromResult(0);
            }

            return NavigateToAsync(forwardStack.Pop());
        }

        public void GoForward()
        {
            GoForwardAsync();
        }

        public Task NavigateToAsync(string pageKey)
        {
            return NavigateToAsync(pageKey, null);
        }

        public Task NavigateToAsync(string pageKey, object parameter)
        {
            Type pageType = GetPageType(pageKey);
            return NavigateToAsync(pageType, parameter);
        }

        public Task NavigateToAsync(Type pageType)
        {
            return NavigateToAsync(pageType, null);
        }

        public Task NavigateToAsync(Type pageType, object parameter)
        {
            object[] arguments = PrepareConstructorArguments(parameter);
            Page page = (Page)reflectionService.CreateInstance(pageType, arguments);
            return NavigateToAsync(page);
        }

        public void NavigateTo(string pageKey)
        {
            NavigateToAsync(pageKey);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            NavigateToAsync(pageKey, parameter);
        }

        public void ProcessMessage(INavigationMessage message)
        {
            if (message == null)
            {
                return;
            }

            message.PerformMessage(this);
        }

        public void Configure(string pageKey, Type pageType)
        {
            if (pageType == null)
            {
                registeredPages.Remove(pageKey);
                return;
            }

            registeredPages[pageKey] = pageType;
        }

        private async Task GoBackWithForwardingAsync()
        {
            Page removedPage = await xamarinNavigation.PopAsync();
            forwardStack.Push(removedPage);
        }

        private Task NavigateToAsync(Page page)
        {
            return xamarinNavigation.PushAsync(page);
        }

        private Type GetPageType(string pageKey)
        {
            if(!registeredPages.ContainsKey(pageKey))
            {
                throw new ArgumentException(
                    $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?",
                    nameof(pageKey));
            }

            return registeredPages[pageKey];
        }

        private static object[] PrepareConstructorArguments(object parameter)
        {
            if (parameter == null)
            {
                return new object[0];
            }

            return new[] { parameter };
        }
    }
}