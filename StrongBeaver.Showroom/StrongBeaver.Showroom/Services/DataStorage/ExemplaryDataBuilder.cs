using SQLite;
using StrongBeaver.Services.Storage.Data;
using StrongBeaver.Showroom.Model;

namespace StrongBeaver.Showroom.Services.DataStorage
{
    public class ExemplaryDataBuilder : BaseDataBuilder
    {
        public ExemplaryDataBuilder(ISqlContext context)
            : base(context, null)
        {
            // no operation
        }

        protected override void OnModelPreparation(SQLiteConnection connection)
        {
            connection.CreateTable<ExemplaryItem>();
        }
    }
}
