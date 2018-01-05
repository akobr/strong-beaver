namespace StrongBeaver.Core.Services.Storage.Data
{
    public interface IEntityFrameworkCoreDataStorageBuilder
    {
        IDataContext BuildDataContext();

        IDataStorageBuilder BuildDataModelBuilder();
    }
}
