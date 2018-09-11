using Foundation;
using StrongBeaver.Core.Services;
using StrongBeaver.Showroom.Services;

namespace StrongBeaver.Showroom.iOS.Services
{
    public class IosWebContentPathService : BaseService, IWebContentPathService
    {
        public string GetPath()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}
