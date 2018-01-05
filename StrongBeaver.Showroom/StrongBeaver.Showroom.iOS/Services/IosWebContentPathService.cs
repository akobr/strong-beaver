using Foundation;
using StrongBeaver.Showroom.Services;

namespace StrongBeaver.Showroom.iOS.Services
{
    public class IosWebContentPathService : IWebContentPathService
    {
        public string GetPath()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}
