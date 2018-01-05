using System;

namespace StrongBeaver.Core.Services.Navigation
{
    public class NavigationConfigureMessage : ServiceMessage, INavigationMessage
    {
        public NavigationConfigureMessage(string pageName, Type pageType)
        {
            PageName = pageName;
            PageType = pageType;
        }

        public string PageName { get; }

        public Type PageType { get; }

        public void PerformMessage(INavigationService service)
        {
            service.Configure(PageName, PageType);
        }
    }
}