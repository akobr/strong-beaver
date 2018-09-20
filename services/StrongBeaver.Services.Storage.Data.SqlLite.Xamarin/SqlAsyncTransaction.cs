using System.Threading.Tasks;
using SQLite;

namespace StrongBeaver.Services.Storage.Data
{
    public class SqlAsyncTransaction : IAsyncTransaction
    {
        private readonly SQLiteConnection connection;
        private bool hasBeenProcessed;

        public SqlAsyncTransaction(SQLiteConnection connection)
        {
            this.connection = connection;
            Begin();
        }

        public Task CommitAsync()
        {
            if (hasBeenProcessed)
            {
                return Task.FromResult(0);
            }

            hasBeenProcessed = true;
            return Task.Run(() => connection.Commit());
        }

        public Task RollbackAsync()
        {
            if (hasBeenProcessed)
            {
                return Task.FromResult(0);
            }

            hasBeenProcessed = true;
            return Task.Run(() => connection.Rollback());
        }

        public void Dispose()
        {
            if (!hasBeenProcessed)
            {
                hasBeenProcessed = true;
                connection.Rollback();
            }

            connection.Dispose();
        }

        private void Begin()
        {
            if (connection.IsInTransaction)
            {
                return;
            }

            connection.BeginTransaction();
        }
    }
}
