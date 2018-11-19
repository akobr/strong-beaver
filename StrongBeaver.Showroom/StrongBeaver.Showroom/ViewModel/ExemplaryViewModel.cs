using System.Collections.ObjectModel;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using StrongBeaver.Core.Commands;
using StrongBeaver.Core.ViewModel;
using StrongBeaver.Services.Storage.Data;
using StrongBeaver.Services.Storage.Json;
using StrongBeaver.Services.Storage.KeyValues;
using StrongBeaver.Showroom.Model;

namespace StrongBeaver.Showroom.ViewModel
{
    public class ExemplaryViewModel : BaseViewModel
    {
        private const string KEY_VALUE_KEY = "42";
        private const string JSON_STORED_ITEM_KEY = "42";

        private readonly IKeyValuesStorageSyncService keyValueStorage;
        private readonly IJsonStorageService jsonStorage;
        private readonly IDataStorageService relationStorage;

        private ExemplaryItem currentItem;
        private ObservableCollection<ExemplaryItem> relationItems;
        private string keyValueData;
        private string jsonData;

        public ExemplaryViewModel(
            IKeyValuesStorageSyncService keyValueStorage,
            IJsonStorageService jsonStorage,
            IDataStorageService relationStorage)
        {
            this.keyValueStorage = keyValueStorage;
            this.jsonStorage = jsonStorage;
            this.relationStorage = relationStorage;
        }

        public ExemplaryItem CurrentItem
        {
            get { return currentItem; }
            set { Set(ref currentItem, value); }
        }

        public ObservableCollection<ExemplaryItem> RelationItems
        {
            get { return relationItems; }
            set { Set(ref relationItems, value); }
        }

        public string KeyValueData
        {
            get { return keyValueData; }
            set { Set(ref keyValueData, value); }
        }

        public string JsonData
        {
            get { return jsonData; }
            set { Set(ref jsonData, value); }
        }

        public ICommand GenerateNewItemCommand => new RelayCommand(GenerateNewItem);

        public ICommand StoreKeyValueCommand => new RelayCommand(StoreKeyValue);

        public ICommand RetrieveKeyValueCommand => new RelayCommand(RetrieveKeyValue);

        public ICommand StoreJsonCommand => new RelayCommand(StoreJson);

        public ICommand RetrieveJsonCommand => new RelayCommand(RetrieveJson);

        public ICommand StoreRelationCommand => new RelayCommand(StoreRelation);

        public ICommand RetrieveRelationCommand => new RelayCommand(RetrieveRelation);

        public override void Initialise()
        {
            base.Initialise();

            // First item
            GenerateNewItem();
            StoreKeyValue();
            StoreRelation();

            // Second item
            GenerateNewItem();
            StoreJson();
            StoreRelation();

            // Third item
            GenerateNewItem();
            StoreRelation();

            // Fourth item
            GenerateNewItem();
            StoreRelation();

            // Fifth item
            GenerateNewItem();
            StoreRelation();

            // Retrieve
            RetrieveKeyValue();
            RetrieveJson();
            RetrieveRelation();
        }

        private void GenerateNewItem()
        {
            CurrentItem = new ExemplaryItem();
        }

        private void StoreKeyValue()
        {
            keyValueStorage.Save(KEY_VALUE_KEY, CurrentItem.ToString());
        }

        private void RetrieveKeyValue()
        {
            KeyValueData = keyValueStorage.Load(KEY_VALUE_KEY);
        }

        private void StoreJson()
        {
            jsonStorage.Store(JSON_STORED_ITEM_KEY, CurrentItem);
        }

        private void RetrieveJson()
        {
            JsonData = jsonStorage.GetObject<JToken>(JSON_STORED_ITEM_KEY)?.ToString();
        }

        private void StoreRelation()
        {
            using (IDataOperation operation = relationStorage.GetOperation())
            {
                operation.Store(CurrentItem);
            }
        }

        private void RetrieveRelation()
        {
            using (IDataOperation operation = relationStorage.GetOperation())
            {
                RelationItems = new ObservableCollection<ExemplaryItem>(operation.GetAll<ExemplaryItem>());
            }
        }
    }
}
