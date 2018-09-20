using System.Collections.Generic;
using System.Linq;
using StrongBeaver.Core.Services;
using Xamarin.Forms;

namespace StrongBeaver.Services.Storage.KeyValues
{
    public class XamarinApplicationKeyValuesStorageService : BaseDisposableService, IKeyValuesStorageSyncService
    {
        private static IDictionary<string, object> Properties => Application.Current.Properties;

        public bool Exists(string key)
        {
            return Contains(key);
        }

        public void Save(string key, string value)
        {
            Store(key, value);
        }

        public void Save(IDictionary<string, string> keyValues)
        {
            foreach (var keyValuePair in keyValues)
            {
                Store(keyValuePair.Key, keyValuePair.Value);
            }
        }

        public string Load(string key)
        {
            return Get(key);
        }

        public IDictionary<string, string> LoadAll(string searchPattern)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();

            foreach (var keyValuePair in Properties)
            {
                if (keyValuePair.Key.Contains(searchPattern))
                {
                    result.Add(keyValuePair.Key, keyValuePair.Value.ToString());
                }
            }

            return result;
        }

        public void Delete(string key)
        {
            Remove(key);
        }

        public void DeleteAll(string searchPattern)
        {
            List<string> keysToDelete = Properties.Keys.Where(key => key.Contains(searchPattern)).ToList();
            DeleteAll(keysToDelete);
        }

        public void DeleteAll(IEnumerable<string> keys)
        {
            foreach (string key in keys)
            {
                Remove(key);
            }
        }

        private static bool Contains(string key)
        {
            return Properties.ContainsKey(key);
        }

        private static string Get(string key)
        {
            return Properties.ContainsKey(key)
                ? Properties[key].ToString()
                : null;
        }

        private static void Store(string key, string value)
        {
            Properties[key] = value;
        }

        private static void Remove(string key)
        {
            Properties.Remove(key);
        }
    }
}