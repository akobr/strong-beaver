using System;

namespace StrongBeaver.Core.Services.Navigation
{
    public class NavigationToPageTypeMessage : ServiceMessage, INavigationMessage
    {
        public NavigationToPageTypeMessage(Type targetPageType)
        {
            PageType = targetPageType;
            Parameter = null;
        }

        public NavigationToPageTypeMessage(Type targetPageType, object parameter)
        {
            PageType = targetPageType;
            Parameter = parameter;
        }

        public NavigationToPageTypeMessage(object sender, Type targetPageType)
            : this(sender, targetPageType, null)
        {
            // Empty
        }

        public NavigationToPageTypeMessage(object sender, Type targetPageType, object parameter)
            : base(sender)
        {
            PageType = targetPageType;
            Parameter = parameter;
        }

        public Type PageType { get; }

        public object Parameter { get; }

        public void PerformMessage(INavigationService service)
        {
            // TODO: [P3] Make this message async
            service.NavigateToAsync(PageType, Parameter);
        }
    }
}