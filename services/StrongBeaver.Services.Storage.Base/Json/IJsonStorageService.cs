using System.Threading.Tasks;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Storage.Json
{
    public interface IJsonStorageService
        : IJsonStorageSyncService, IJsonStorageAsyncService
    {
        // No members
    }

    public interface IJsonStorageSyncService : IService
    {
        void Store(string key, object data);

        TData GetObject<TData>(string key)
            where TData : class;

        void Delete(string key);
    }

    public interface IJsonStorageAsyncService : IService
    {
        Task StoreAsync(string key, object data);

        Task<TData> GetObjectAsync<TData>(string key)
            where TData : class;

        Task DeleteAsync(string key);
    }
}