using System.IO;
using System.Threading.Tasks;
using StrongBeaver.Core.Extensions;
using StrongBeaver.Core.Services.Serialisation.Json;
using StrongBeaver.Core.Services.Storage.KeyValues;

namespace StrongBeaver.Core.Services.Storage.Json
{
    public class XamarinApplicationJsonStorageService : BaseService, IJsonStorageService
    {
        private const string JSON_OBJECT_KEY_FORMAT = "JSON{0:000}";

        private readonly IJsonSerialisationService jsonSerialization;
        private readonly IKeyValuesStorageSyncService keyValueStore;

        public XamarinApplicationJsonStorageService(IJsonSerialisationService jsonSerialization, IKeyValuesStorageSyncService keyValueStore)
        {
            this.jsonSerialization = jsonSerialization;
            this.keyValueStore = keyValueStore;
        }

        public void Store(int id, object data)
        {
            using (Stream stream = new MemoryStream())
            {
                jsonSerialization.SerializeToStream(data, stream);
                stream.Position = 0;

                using (TextReader reader = new StreamReader(stream))
                {
                    keyValueStore.Save(GetObjectStoreKey(id), reader.ReadToEnd());
                }
            }
        }

        public async Task StoreAsync(int id, object data)
        {
            using (Stream stream = new MemoryStream())
            {
                await jsonSerialization.SerializeToStreamAsync(data, stream);
                stream.Position = 0;

                using (TextReader reader = new StreamReader(stream))
                {
                    keyValueStore.Save(GetObjectStoreKey(id), await reader.ReadToEndAsync());
                }
            }
        }

        public TData GetObject<TData>(int id)
            where TData : class
        {
            string storedValue = keyValueStore.Load(GetObjectStoreKey(id));

            if (string.IsNullOrEmpty(storedValue))
            {
                return null;
            }

            using (Stream stream = storedValue.ToStream())
            {
                return jsonSerialization.DeserializeFromStream<TData>(stream);
            }
        }

        public async Task<TData> GetObjectAsync<TData>(int id)
            where TData : class
        {
            string storedValue = keyValueStore.Load(GetObjectStoreKey(id));

            if (string.IsNullOrEmpty(storedValue))
            {
                return null;
            }

            using (Stream stream = await storedValue.ToStreamAsync())
            {
                return await jsonSerialization.DeserializeFromStreamAsync<TData>(stream);
            }
        }

        public void Delete(int id)
        {
            keyValueStore.Delete(GetObjectStoreKey(id));
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }

        private static string GetObjectStoreKey(int id)
        {
            return string.Format(JSON_OBJECT_KEY_FORMAT, id);
        }
    }
}