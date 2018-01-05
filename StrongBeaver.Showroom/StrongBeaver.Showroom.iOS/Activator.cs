using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
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
            ISimpleIoc serviceLocator = SimpleIoc.Default;

            InitializeIoc(serviceLocator);

            DispatcherHelper.Initialize(application);

            Core.Activator.Initialise(serviceLocator);
            Showroom.Activator.Initialise(serviceLocator);
        }

        private static void InitializeIoc(ISimpleIoc container)
        {
            Core.IosActivator.InitialiseIoc(container);
            Showroom.Activator.InitialiseIoc(container);

            InitialisePlatform(container);
            InitialiseServices(container);
        }

        private static void InitialisePlatform(ISimpleIoc container)
        {
            container.Register<IApplicationInfo, ShowroomIosApplicationInfo>();
        }

        private static void InitialiseServices(ISimpleIoc container)
        {
            container.Register<ISqlContext, IosSQLiteContext>();
            container.Register<IWebContentPathService, IosWebContentPathService>();
        }
    }
}
