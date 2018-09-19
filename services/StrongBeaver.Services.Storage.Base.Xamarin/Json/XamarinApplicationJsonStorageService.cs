using System.IO;
using System.Threading.Tasks;
using StrongBeaver.Core.Extensions;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Serialisation.Json;
using StrongBeaver.Services.Storage.KeyValues;

namespace StrongBeaver.Services.Storage.Json
{
    public class XamarinApplicationJsonStorageService : BaseDisposableService, IJsonStorageService
    {
        private const string JSON_OBJECT_KEY_PREFIX = "[JSON]";

        private readonly IJsonSerialisationService jsonSerialization;
        private readonly IKeyValuesStorageSyncService keyValueStore;

        public XamarinApplicationJsonStorageService(IJsonSerialisationService jsonSerialization, IKeyValuesStorageSyncService keyValueStore)
        {
            this.jsonSerialization = jsonSerialization;
            this.keyValueStore = keyValueStore;
        }

        public void Store(string key, object data)
        {
            using (Stream stream = new MemoryStream())
            {
                jsonSerialization.SerializeToStream(data, stream);
                stream.Position = 0;

                using (TextReader reader = new StreamReader(stream))
                {
                    keyValueStore.Save(GetObjectStoreKey(key), reader.ReadToEnd());
                }
            }
        }

        public async Task StoreAsync(string key, object data)
        {
            using (Stream stream = new MemoryStream())
            {
                await jsonSerialization.SerializeToStreamAsync(data, stream);
                stream.Position = 0;

                using (TextReader reader = new StreamReader(stream))
                {
                    keyValueStore.Save(GetObjectStoreKey(key), await reader.ReadToEndAsync());
                }
            }
        }

        public TData GetObject<TData>(string key)
            where TData : class
        {
            string storedValue = keyValueStore.Load(GetObjectStoreKey(key));

            if (string.IsNullOrEmpty(storedValue))
            {
                return null;
            }

            using (Stream stream = storedValue.ToStream())
            {
                return jsonSerialization.DeserializeFromStream<TData>(stream);
            }
        }

        public async Task<TData> GetObjectAsync<TData>(string key)
            where TData : class
        {
            string storedValue = keyValueStore.Load(GetObjectStoreKey(key));

            if (string.IsNullOrEmpty(storedValue))
            {
                return null;
            }

            using (Stream stream = await storedValue.ToStreamAsync())
            {
                return await jsonSerialization.DeserializeFromStreamAsync<TData>(stream);
            }
        }

        public void Delete(string key)
        {
            keyValueStore.Delete(GetObjectStoreKey(key));
        }

        public Task DeleteAsync(string key)
        {
            return Task.Run(() => Delete(key));
        }

        private static string GetObjectStoreKey(string key)
        {
            return JSON_OBJECT_KEY_PREFIX + key;
        }
    }
}