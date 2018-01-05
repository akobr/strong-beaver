using GalaSoft.MvvmLight.Ioc;
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
        public static void Initialise(ISimpleIoc container)
        {
            InitialiseLocalDatabase(container);
        }

        public static void InitialiseIoc(ISimpleIoc container)
        {
            InitiliaseServices(container);
            InitialiseStores(container);
            InitialiseViewModels(container);
        }

        private static void InitiliaseServices(ISimpleIoc container)
        {
            // SQLite persistent storage
            container.Register<IDataStorageBuilder, ExemplaryDataStorageBuilder>();
            container.Register<IDataStorageService, SQLiteDataStorageService>();

            container.Register<IPermissionsService, XamarinPermissionsPluginService>();
            container.Register<IConnectivityService, XamarinConnectivityPluginService>();
            container.Register<IGeolocatorService, XamarinGeolocatorPluginService>();
        }

        private static void InitialiseStores(ISimpleIoc container)
        {
            container.Register<ExemplaryStore>();
        }

        private static void InitialiseViewModels(ISimpleIoc container)
        {
            container.Register<IMainViewModel, MainViewModel>();
            container.Register<ExemplaryViewModel>();
        }

        private static void InitialiseLocalDatabase(ISimpleIoc container)
        {
            IDataStorageService dataStorage = container.GetInstance<IDataStorageService>();

            using (IDataStorageBuilder modelBuilder = dataStorage.GetDataModelBuilder())
            {
                modelBuilder.EnsureModelAsync();
            }
        }
    }
}
