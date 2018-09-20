using System.Collections.Generic;
using System.Threading.Tasks;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Storage.KeyValues
{
    public interface IKeyValuesStorageService
        : IKeyValuesStorageSyncService, IKeyValuesStorageAsyncService
    {
        // No members
    }

    public interface IKeyValuesStorageSyncService : IService
    {
        bool Exists(string key);

        void Save(string key, string value);

        void Save(IDictionary<string, string> keyValues);

        string Load(string key);

        IDictionary<string, string> LoadAll(string searchPattern);

        void Delete(string key);

        void DeleteAll(string searchPattern);

        void DeleteAll(IEnumerable<string> keys);
    }

    public interface IKeyValuesStorageAsyncService : IService
    {
        Task<bool> ExistsAsync(string key);

        Task SaveAsync(string key, string value);

        Task SaveAsync(IDictionary<string, string> keyValues);

        Task<string> LoadAsync(string key);

        Task<IDictionary<string, string>> LoadAllAsync(string searchPattern);

        Task DeleteAsync(string key);

        Task DeleteAllAsync(string searchPattern);

        Task DeleteAllAsync(IEnumerable<string> keys);
    }
}