using Microsoft.Practices.ServiceLocation;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class SQLiteDataStorageService : BaseService, IDataStorageService
    {
        public IDataStorageBuilder GetDataModelBuilder()
        {
            return BuildDataModelBuilder();
        }

        public IAsyncDataOperation GetAsyncDataOperation()
        {
            return new DefaultAsyncDataOperation(BuildContext());
        }

        public IDataOperation GetDataOperation()
        {
            return new DefaultDataOperation(BuildContext());
        }

        protected virtual ISqlContext BuildContext()
        {
            return ServiceLocator.Current.GetInstance<ISqlContext>();
        }

        protected virtual IDataStorageBuilder BuildDataModelBuilder()
        {
            return ServiceLocator.Current.GetInstance<IDataStorageBuilder>();
        }
    }
}
