using StrongBeaver.Core;
using StrongBeaver.Core.Services.Logging;
using UIKit;

namespace StrongBeaver.Showroom.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (System.Exception ex)
            {
                App.Logger.Error("Unhandled exception occurred.", ex);
                throw;
            }
        }
    }
}
