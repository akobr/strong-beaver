using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using StrongBeaver.Core.Container;
using StrongBeaver.Showroom.iOS.Platform;
using StrongBeaver.Core.Platform;
using UIKit;
using StrongBeaver.Showroom.Services;
using StrongBeaver.Showroom.iOS.Services;
using StrongBeaver.Core.Services.Storage.Data;
using StrongBeaver.Showroom.iOS.Services.DataStorage;

namespace StrongBeaver.Showroom.iOS
{
    public static class Activator
    {
        public static void Initialise(UIApplication application)
        {
            SimpleServiceLocator container = new SimpleServiceLocator();

            InitializeIoc(container);

            DispatcherHelper.Initialize(application);

            Core.Activator.Initialise(container);
            Showroom.Activator.Initialise(container);
        }

        private static void InitializeIoc(IContainer container)
        {
            Core.IosActivator.InitialiseIoc(container);
            Showroom.Activator.InitialiseIoc(container);

            InitialisePlatform(container);
            InitialiseServices(container);
        }

        private static void InitialisePlatform(IContainer container)
        {
            container.Register<IApplicationInfo, ShowroomIosApplicationInfo>();
        }

        private static void InitialiseServices(IContainer container)
        {
            container.Register<ISqlContext, IosSqLiteContext>();
            container.Register<IWebContentPathService, IosWebContentPathService>();
        }
    }
}
