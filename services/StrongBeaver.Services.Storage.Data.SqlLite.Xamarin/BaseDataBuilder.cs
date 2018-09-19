using System;
using System.Threading.Tasks;
using SQLite;
using StrongBeaver.Services.Storage.Files;

namespace StrongBeaver.Services.Storage.Data
{
    public abstract class BaseDataBuilder : IDataBuilder
    {
        private readonly ISqlContext context;
        private readonly IFileStorageService fileStorageService;

        protected BaseDataBuilder(ISqlContext context, IFileStorageService fileStorageService)
        {
            this.context = context;
            this.fileStorageService = fileStorageService;
        }

        public Task BuildStorageAsync()
        {
            return Task.Run(() => BuildModel());
        }

        public Task DeleteStorageAsync()
        {
            if (fileStorageService == null)
            {
                return Task.FromResult(0);
            }

            return fileStorageService.DeleteFileAsync(context.DatabasePath);
        }

        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose(bool disposing)
        {
            // no operation ( template method )
        }

        protected abstract void OnModelPreparation(SQLiteConnection connection);

        private SQLiteConnection BuildConnection()
        {
            return new SQLiteConnection(context.DatabasePath);
        }

        private void BuildModel()
        {
            using (SQLiteConnection connection = BuildConnection())
            {
                OnModelPreparation(connection);
            }
        }
    }
}
