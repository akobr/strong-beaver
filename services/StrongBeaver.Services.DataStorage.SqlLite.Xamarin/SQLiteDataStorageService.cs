namespace StrongBeaver.Core.Services.Storage.Data
{
    public class SqLiteDataStorageService : BaseService, IDataStorageService
    {
        private readonly ISqlContext sqlContext;
        private readonly IDataStorageBuilder storageBuilder;

        public SqLiteDataStorageService(ISqlContext sqlContext, IDataStorageBuilder storageBuilder)
        {
            this.sqlContext = sqlContext;
            this.storageBuilder = storageBuilder;
        }

        public IDataStorageBuilder GetDataModelBuilder()
        {
            return storageBuilder;
        }

        public IAsyncDataOperation GetAsyncDataOperation()
        {
            return new DefaultAsyncDataOperation(sqlContext);
        }

        public IDataOperation GetDataOperation()
        {
            return new DefaultDataOperation(sqlContext);
        }
    }
}
