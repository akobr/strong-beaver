
using StrongBeaver.Core.Services.Storage.File;
using System.Threading.Tasks;
using SQLite;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public abstract class BaseDataStorageBuilder : IDataStorageBuilder
    {
        private readonly ISqlContext context;

        protected BaseDataStorageBuilder(ISqlContext context)
        {
            this.context = context;
        }

        public void EnsureDeleted()
        {
            FileStorageDeleteFileMessage deleteMessage = new FileStorageDeleteFileMessage(context.DatabasePath);
            ServiceProvider.Current.Messanger.Send<IFileStorageMessage>(deleteMessage);

            if (deleteMessage.OperationHolder.HasAssignedOperation)
            {
                deleteMessage.OperationHolder.Operation.Wait();
            }
        }

        public Task EnsureDeletedAsync()
        {
            FileStorageDeleteFileMessage deleteMessage = new FileStorageDeleteFileMessage(context.DatabasePath);
            ServiceProvider.Current.Messanger.Send<IFileStorageMessage>(deleteMessage);

            if (!deleteMessage.OperationHolder.HasAssignedOperation)
            {
                return Task.FromResult(0);
            }

            return deleteMessage.OperationHolder.Operation;
        }

        public void EnsureModel()
        {
            using (SQLiteConnection connection = BuildConnection())
            {
                OnModelPreparation(connection);
            }
        }

        public Task EnsureModelAsync()
        {
            return Task.Run(() => EnsureModel());
        }

        public virtual void Dispose()
        {
            // No operation
        }

        protected abstract void OnModelPreparation(SQLiteConnection connection);

        private SQLiteConnection BuildConnection()
        {
            return new SQLiteConnection(context.DatabasePath);
        }
    }
}
