using StrongBeaver.Core.Container;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.Services.Dialog;
using StrongBeaver.Core.Services.Localisation;

namespace StrongBeaver.Core
{
    public static class IosActivator
    {
        public static void InitialiseIoc(IContainer iocContainer)
        {
            Activator.InitialiseIoc(iocContainer);

            InitialisePlatform(iocContainer);
            InitialiseServices(iocContainer);
        }

        private static void InitialisePlatform(IContainer iocContainer)
        {
            iocContainer.Register<IDeviceInfo, IosDeviceInfo>();
            iocContainer.Register<IPlatformInfo, IosPlatformInfo>();
        }

        private static void InitialiseServices(IContainer iocContainer)
        {
            iocContainer.Register<ILocalisationService, IosLocalisationService>();
            iocContainer.Register<IPlatformSpecificDialogService, IosSpecificDialogService>();
        }
    }
}
