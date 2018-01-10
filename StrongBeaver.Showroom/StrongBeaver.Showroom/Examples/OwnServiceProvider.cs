using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Core.Services.Permissions;

namespace StrongBeaver.Showroom.Examples
{
    public interface IOwnServiceProvider : IServiceProvider
    {
        INavigationService Navigation { get; }

        IDialogService Dialog { get; }

        IPermissionsService Permissions { get; }
    }

    public class OwnServiceProvider : ServiceProvider, IOwnServiceProvider
    {
        public OwnServiceProvider(ISimpleIoc container, ILogService logService)
          : base(container, logService)
        {
            Navigation = container.GetInstance<INavigationService>();
            Dialog = container.GetInstance<IDialogService>();
            Permissions = container.GetInstance<IPermissionsService>();
        }

        public INavigationService Navigation { get; }

        public IDialogService Dialog { get; }

        public IPermissionsService Permissions { get; }

        public new static IOwnServiceProvider Current { get; private set; }

        public static void SetDefaultProvider()
        {
            SetProvider(ServiceLocator.Current.GetInstance<IOwnServiceProvider>());
        }

        public static void SetProvider(IOwnServiceProvider newProvider)
        {
            Current = newProvider;
        }
    }
}
