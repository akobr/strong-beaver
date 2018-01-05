using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class MigrationDataStorageBuilder : IDataStorageBuilder
    {
        private readonly IDataContext context;

        public MigrationDataStorageBuilder(IDataContext context)
        {
            this.context = context;
        }

        public void EnsureDeleted()
        {
            context.Database.EnsureDeleted();
        }

        public Task EnsureDeletedAsync()
        {
            return context.Database.EnsureDeletedAsync();
        }

        public void EnsureModel()
        {
            context.Database.Migrate();
        }

        public Task EnsureModelAsync()
        {
            return context.Database.MigrateAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
