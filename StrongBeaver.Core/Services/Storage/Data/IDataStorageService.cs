namespace StrongBeaver.Core.Services.Storage.Data
{
    public interface IDataStorageService : IService
    {
        IDataOperation GetDataOperation();

        IAsyncDataOperation GetAsyncDataOperation();

        IDataStorageBuilder GetDataModelBuilder();
    }
}