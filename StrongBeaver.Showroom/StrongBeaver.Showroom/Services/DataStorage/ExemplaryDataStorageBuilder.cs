using SQLite;
using StrongBeaver.Core.Services.Storage.Data;
using StrongBeaver.Showroom.Model;

namespace StrongBeaver.Showroom.Services.DataStorage
{
    public class ExemplaryDataStorageBuilder : BaseDataStorageBuilder
    {
        public ExemplaryDataStorageBuilder(ISqlContext context)
            : base(context)
        {
            // No operation
        }

        protected override void OnModelPreparation(SQLiteConnection connection)
        {
            connection.CreateTable<ExemplaryItem>();
        }
    }
}
