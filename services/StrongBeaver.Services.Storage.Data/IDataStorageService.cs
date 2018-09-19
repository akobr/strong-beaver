using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Storage.Data
{
    public interface IDataStorageService : IService
    {
        IDataOperation GetOperation();

        IAsyncDataOperation GetAsyncOperation();

        IDataBuilder GetBuilder();
    }
}