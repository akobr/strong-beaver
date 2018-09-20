using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Navigation
{
    public class NavigateToPageNameMesage : ServiceAsyncMesssage, INavigationMessage
    {
        public NavigateToPageNameMesage(string targetPageName, object parameter)
        {
            PageName = targetPageName;
            Parameter = parameter;
        }

        public NavigateToPageNameMesage(string targetPageName)
            : this(targetPageName, null)
        {
            // no operation
        }

        public string PageName { get; }

        public object Parameter { get; }

        public void PerformMessage(INavigationService service)
        {
            OperationHolder = new TaskHolder(service.NavigateToAsync(PageName, Parameter));
        }
    }
}