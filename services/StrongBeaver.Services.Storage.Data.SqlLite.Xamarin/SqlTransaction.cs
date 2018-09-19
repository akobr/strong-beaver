using SQLite;

namespace StrongBeaver.Services.Storage.Data
{
    public class SqlTransaction : ITransaction
    {
        private readonly SQLiteConnection connection;
        private bool hasBeenProcessed;

        public SqlTransaction(SQLiteConnection connection)
        {
            this.connection = connection;
            Begin();
        }

        public void Commit()
        {
            if (hasBeenProcessed)
            {
                return;
            }

            hasBeenProcessed = true;
            connection.Commit();
        }

        public void Rollback()
        {
            if (hasBeenProcessed)
            {
                return;
            }

            hasBeenProcessed = true;
            connection.Rollback();
        }

        public void Dispose()
        {
            if (!hasBeenProcessed)
            {
                Rollback();
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
