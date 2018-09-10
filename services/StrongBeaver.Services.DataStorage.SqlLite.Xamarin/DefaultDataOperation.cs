using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class DefaultDataOperation : IDataOperation
    {
        private readonly SQLiteConnection connection;

        public DefaultDataOperation(ISqlContext context)
        {
            connection = new SQLiteConnection(context.DatabasePath);
        }

        public void StartTransaction()
        {
            if (connection.IsInTransaction)
            {
                return;
            }

            connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            connection.Commit();
        }

        public void RollbackTransaction()
        {
            connection.Rollback();
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
                if (!connection.IsInTransaction)
                {
                    return;
                }

                connection.Commit();
            }
            catch (Exception exception)
            {
                Provider.LogErrorMessage("Exception occured during data operation.", exception);
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
