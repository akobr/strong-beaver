using GalaSoft.MvvmLight.Ioc;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.Services.Dialog;
using StrongBeaver.Core.Services.Localisation;

namespace StrongBeaver.Core
{
    public static class IosActivator
    {
        public static void InitialiseIoc(ISimpleIoc iocContainer)
        {
            Activator.InitialiseIoc(iocContainer);

            InitialisePlatform(iocContainer);
            InitialiseServices(iocContainer);
        }

        private static void InitialisePlatform(ISimpleIoc iocContainer)
        {
            iocContainer.Register<IDeviceInfo, IosDeviceInfo>();
            iocContainer.Register<IPlatformInfo, IosPlatformInfo>();
        }

        private static void InitialiseServices(ISimpleIoc iocContainer)
        {
            iocContainer.Register<ILocalisationService, IosLocalisationService>();
            iocContainer.Register<IPlatformSpecificDialogService, IosSpecificDialogService>();
        }
    }
}
