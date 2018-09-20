using System;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Navigation
{
    public class NavigateToPageTypeMessage : ServiceAsyncMesssage, INavigationMessage
    {
        public NavigateToPageTypeMessage(Type targetPageType, object parameter)
        {
            PageType = targetPageType;
            Parameter = parameter;
        }

        public NavigateToPageTypeMessage(Type targetPageType)
            : this(targetPageType, null)
        {
            // no operation
        }

        public Type PageType { get; }

        public object Parameter { get; }

        public void PerformMessage(INavigationService service)
        {
            OperationHolder = new TaskHolder(service.NavigateToAsync(PageType, Parameter));
        }
    }
}