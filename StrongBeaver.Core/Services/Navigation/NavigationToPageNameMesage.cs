namespace StrongBeaver.Core.Services.Navigation
{
    public class NavigationToPageNameMesage : ServiceMessage, INavigationMessage
    {
        public NavigationToPageNameMesage(string targetPageName)
        {
            PageName = targetPageName;
            Parameter = null;
        }

        public NavigationToPageNameMesage(string targetPageName, object parameter)
        {
            PageName = targetPageName;
            Parameter = parameter;
        }

        public NavigationToPageNameMesage(object sender, string targetPageName)
            : this(sender, targetPageName, null)
        {
            // Empty
        }

        public NavigationToPageNameMesage(object sender, string targetPageName, object parameter)
            : base(sender)
        {
            PageName = targetPageName;
            Parameter = parameter;
        }

        public string PageName { get; }

        public object Parameter { get; }

        public void PerformMessage(INavigationService service)
        {
            service.NavigateTo(PageName, Parameter);
        }
    }
}