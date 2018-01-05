using Microsoft.Practices.ServiceLocation;
using StrongBeaver.Showroom.Services;

namespace StrongBeaver.Showroom.View.WebContent
{
    public class LocalWebContentDescription : IWebContentDescription
    {
        public string Title { get; set; }

        public string PageName { get; set; }

        public string Path
        {
            get
            {
                IWebContentPathService webContentPathService = ServiceLocator.Current.GetInstance<IWebContentPathService>();
                return webContentPathService.GetPath() + "/" + PageName;
            }
        }
    }
}
