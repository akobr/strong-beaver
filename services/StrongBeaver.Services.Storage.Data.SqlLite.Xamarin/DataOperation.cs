using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Services.Storage.Data
{
    public class DataOperation : IDataOperation
    {
        private readonly ILogService logging;
        private readonly SQLiteConnection connection;
        private ITransaction transaction;

        public DataOperation(ISqlContext context, ILogService logging)
        {
            this.logging = logging;
            connection = new SQLiteConnection(context.DatabasePath);
        }

        public ITransaction Transaction
        {
            get
            {
                if (transaction == null)
                {
                    BuildTransaction();
                }

                return transaction;
            }
        }

        private void BuildTransaction()
        {
            if (transaction != null)
            {
                return;
            }

            transaction = new SqlTransaction(connection);
        }

        public TData Get<TData>(object primaryKey)
            where TData : new()
        {
            return connection.Find<TData>(primaryKey);
        }

        public TData Get<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new()
        {
            return connection.Find<TData>(predicate);
        }

        public IEnumerable<TData> GetAll<TData>()
            where TData : new()
        {
            return connection.Table<TData>();
        }

        public IEnumerable<TData> GetAll<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new()
        {
            return connection.Table<TData>().Where(predicate);
        }

        public void Store<TData>(TData item)
            where TData : new()
        {
            connection.InsertOrReplace(item);
        }

        public void Store<TData>(IEnumerable<TData> items)
            where TData : new()
        {
            foreach (TData item in items)
            {
                connection.InsertOrReplace(item);
            }
        }

        public bool Delete<TData>(TData item)
            where TData : new()
        {
            return connection.Delete(item) > 0;
        }

        public bool Delete<TData>(object primaryKey)
            where TData : new()
        {
            return connection.Delete<TData>(primaryKey) > 0;
        }

        public int DeleteAll<TData>()
            where TData : new()
        {
            return connection.DeleteAll<TData>();
        }

        public int DeleteAll<TData>(IEnumerable<TData> items)
            where TData : new()
        {
            int result = 0;

            foreach (TData item in items)
            {
                if (Delete<TData>(item))
                {
                    result++;
                }
            }

            return result;
        }

        public int DeleteAll<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new()
        {
            int result = 0;

            foreach (TData item in GetAll<TData>().ToList())
            {
                if (Delete<TData>(item))
                {
                    result++;
                }
            }

            return result;
        }

        public void Save()
        {
            try
            {
                if (transaction == null)
                {
                    return;
                }

                transaction.Commit();
            }
            catch (Exception exception)
            {
                logging.Error("Exception occured during data operation.", exception);
                throw;
            }
        }

        public Task SaveAsync()
        {
            return Task.Run(() => Save());
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
