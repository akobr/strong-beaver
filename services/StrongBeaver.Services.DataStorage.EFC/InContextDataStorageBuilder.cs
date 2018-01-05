using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class InContextDataStorageBuilder : IDataStorageBuilder
    {
        private readonly IDataContext context;

        public InContextDataStorageBuilder(IDataContext context)
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
            context.Database.EnsureCreated();
        }

        public Task EnsureModelAsync()
        {
            return context.Database.EnsureCreatedAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
