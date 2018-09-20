using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using StrongBeaver.Core;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Services.Storage.Data
{
    public class AsyncDataOperation : IAsyncDataOperation
    {
        private readonly object locker = new object();
        private readonly ILogService logging;
        private readonly ISqlContext sqlContext;
        private readonly SQLiteAsyncConnection asyncConnection;

        private SQLiteConnectionWithLock connection;

        private IAsyncTransaction transaction;

        public AsyncDataOperation(ISqlContext context, ILogService logging)
        {
            this.logging = logging;
            sqlContext = context;
            asyncConnection = new SQLiteAsyncConnection(context.DatabasePath);
            connection = asyncConnection.GetConnection();
        }

        public IAsyncTransaction Transaction
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

        public async Task<IList<TData>> GetAllAsync<TData>()
            where TData : new()
        {
            return await asyncConnection.Table<TData>().ToListAsync();
        }

        public async Task<IList<TData>> GetAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new()
        {
            return await asyncConnection.Table<TData>().Where(predicate).ToListAsync();
        }

        public Task<TData> GetAsync<TData>(object primaryKey)
            where TData : new()
        {
            return asyncConnection.FindAsync<TData>(primaryKey);
        }

        public Task<TData> GetAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new()
        {
            return asyncConnection.FindAsync<TData>(predicate);
        }

        public Task StoreAsync<TData>(TData item)
            where TData : new()
        {
            return asyncConnection.InsertOrReplaceAsync(item);
        }

        public async Task StoreAsync<TData>(IEnumerable<TData> items)
            where TData : new()
        {
            foreach(TData item in items)
            {
                await asyncConnection.InsertOrReplaceAsync(item);
            }
        }

        public async Task<int> DeleteAllAsync<TData>()
            where TData : new()
        {
            List<TData> allItems = await asyncConnection.Table<TData>().ToListAsync();
            return await DeleteAllAsync<TData>(allItems);
        }

        public async Task<int> DeleteAllAsync<TData>(IEnumerable<TData> items)
            where TData : new()
        {
            int result = 0;

            foreach (TData item in items)
            {
                if (await DeleteAsync<TData>(item))
                {
                    result++;
                }
            }

            return result;
        }

        public async Task<int> DeleteAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new()
        {
            int result = 0;

            foreach (TData item in await GetAllAsync<TData>(predicate))
            {
                if (await DeleteAsync<TData>(item))
                {
                    result++;
                }
            }

            return result;
        }

        public async Task<bool> DeleteAsync<TData>(object primaryKey)
            where TData : new()
        {
            TData item = await GetAsync<TData>(primaryKey);

            if (item == null)
            {
                return false;
            }

            return await asyncConnection.DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteAsync<TData>(TData item)
            where TData : new()
        {
            return await asyncConnection.DeleteAsync(item) > 0;
        }

        public Task SaveAsync()
        {
            try
            {
                if (transaction == null)
                {
                    return Task.FromResult(0);
                }

                return transaction.CommitAsync();
            }
            catch (Exception exception)
            {
                logging.Error("Exception occured during data operation.", exception);
                throw;
            }
        }

        public void Dispose()
        {
            connection?.Dispose();
            connection = null;
        }

        private void BuildTransaction()
        {
            lock (locker)
            {
                if (transaction != null)
                {
                    return;
                }

                transaction = new SqlAsyncTransaction(connection);
            }
        }
    }
}
