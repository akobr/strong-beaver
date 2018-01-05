using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Json
{
    public interface IJsonStorageService
        : IJsonStorageSyncService, IJsonStorageAsyncService
    {
        // No members
    }

    public interface IJsonStorageSyncService : IService
    {
        void Store(int id, object data);

        TData GetObject<TData>(int id)
            where TData : class;

        void Delete(int id);
    }

    public interface IJsonStorageAsyncService : IService
    {
        Task StoreAsync(int id, object data);

        Task<TData> GetObjectAsync<TData>(int id)
            where TData : class;

        Task DeleteAsync(int id);
    }
}