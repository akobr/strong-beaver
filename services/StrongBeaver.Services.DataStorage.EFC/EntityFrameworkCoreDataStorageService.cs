namespace StrongBeaver.Core.Services.Storage.Data
{
    public class EntityFrameworkCoreDataStorageService : BaseService, IDataStorageService
    {
        private readonly IEntityFrameworkCoreDataStorageBuilder builder;

        public EntityFrameworkCoreDataStorageService(IEntityFrameworkCoreDataStorageBuilder builder)
        {
            this.builder = builder;
        }

        public IDataStorageBuilder GetDataModelBuilder()
        {
            return builder.BuildDataModelBuilder();
        }

        public IAsyncDataOperation GetAsyncDataOperation()
        {
            return new DefaultAsyncDataOperation(builder.BuildDataContext());
        }

        public IDataOperation GetDataOperation()
        {
            return new DefaultDataOperation(builder.BuildDataContext());
        }
    }
}
