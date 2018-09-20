
using StrongBeaver.Core.Container;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.Services.Device.Connectivity;
using StrongBeaver.Core.Services.Device.Geolocator;
using StrongBeaver.Core.Services.Permissions;
using StrongBeaver.Core.Services.Storage.Data;
using StrongBeaver.Showroom.Model;
using StrongBeaver.Showroom.Services.DataStorage;
using StrongBeaver.Showroom.ViewModel;

namespace StrongBeaver.Showroom
{
    public static class Activator
    {
        public static void Initialise(IContainer container)
        {
            InitialiseLocalDatabase(container);
            App.SetPlatformModel(container.GetInstance<IPlatformModel>());
        }

        public static void InitialiseIoc(IContainer container)
        {
            InitiliaseServices(container);
            InitialiseStores(container);
            InitialiseViewModels(container);
        }

        private static void InitiliaseServices(IContainer container)
        {
            // SQLite persistent storage
            container.Register<IDataStorageBuilder, ExemplaryDataStorageBuilder>();
            container.Register<IDataStorageService, SqLiteDataStorageService>();

            container.Register<IPermissionsService, XamarinPermissionsPluginService>();
            container.Register<IConnectivityService, XamarinConnectivityPluginService>();
            container.Register<IGeolocatorService, XamarinGeolocatorPluginService>();
        }

        private static void InitialiseStores(IContainer container)
        {
            container.Register<ExemplaryStore>();
        }

        private static void InitialiseViewModels(IContainer container)
        {
            container.Register<IMainViewModel, MainViewModel>();
            container.Register<ExemplaryViewModel>();
        }

        private static void InitialiseLocalDatabase(IContainer container)
        {
            IDataStorageService dataStorage = container.GetInstance<IDataStorageService>();

            using (IDataStorageBuilder modelBuilder = dataStorage.GetDataModelBuilder())
            {
                modelBuilder.EnsureModelAsync();
            }
        }
    }
}
