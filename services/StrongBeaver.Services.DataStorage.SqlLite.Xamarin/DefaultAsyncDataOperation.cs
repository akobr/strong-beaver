﻿using SQLite.Net;
using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class DefaultAsyncDataOperation : IAsyncDataOperation
    {
        private readonly object locker = new object();
        private readonly ISqlContext sqlContext;
        private readonly SQLiteAsyncConnection asyncConnection;

        private SQLiteConnectionWithLock connection;

        public DefaultAsyncDataOperation(ISqlContext context)
        {
            sqlContext = context;
            asyncConnection = new SQLiteAsyncConnection(GetConnection);
        }

        private SQLiteConnectionWithLock GetConnection()
        {
            if (connection == null)
            {
                lock (locker)
                {
                    BuildConnection();
                }
            }

            return connection;
        }

        private void BuildConnection()
        {
            if (connection != null)
            {
                return;
            }

            connection = new SQLiteConnectionWithLock(
                sqlContext.SqlPlatform,
                new SQLiteConnectionString(sqlContext.DatabasePath, true));
        }

        public Task StartTransactionAsync()
        {
            return Task.Run(() => GetConnection().BeginTransaction());
        }

        public Task CommitTransactionAsync()
        {
            return Task.Run(() => GetConnection().Commit());
        }

        public Task RollbackTransactionAsync()
        {
            return Task.Run(() => GetConnection().Rollback());
        }

        public async Task<IList<TData>> GetAllAsync<TData>()
            where TData : class
        {
            return await asyncConnection.Table<TData>().ToListAsync();
        }

        public async Task<IList<TData>> GetAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class
        {
            return await asyncConnection.Table<TData>().Where(predicate).ToListAsync();
        }

        public Task<TData> GetAsync<TData>(object primaryKey)
            where TData : class
        {
            return asyncConnection.FindAsync<TData>(primaryKey);
        }

        public Task<TData> GetAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class
        {
            return asyncConnection.FindAsync<TData>(predicate);
        }

        public Task StoreAsync<TData>(TData item)
            where TData : class
        {
            return asyncConnection.InsertOrReplaceAsync(item);
        }

        public Task StoreAsync<TData>(IEnumerable<TData> items)
            where TData : class
        {
            return asyncConnection.InsertOrReplaceAllAsync(items);
        }

        public Task<int> DeleteAllAsync<TData>()
            where TData : class
        {
            return asyncConnection.DeleteAllAsync<TData>();
        }

        public async Task<int> DeleteAllAsync<TData>(IEnumerable<TData> items)
            where TData : class
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
            where TData : class
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
            where TData : class
        {
            return await asyncConnection.DeleteAsync<TData>(primaryKey) > 0;
        }

        public async Task<bool> DeleteAsync<TData>(TData item)
            where TData : class
        {
            return await asyncConnection.DeleteAsync(item) > 0;
        }

        public Task SaveAsync()
        {
            try
            {
                if (!connection.IsInTransaction)
                {
                    return Task.FromResult(0);
                }

                return CommitTransactionAsync();
            }
            catch (Exception exception)
            {
                Provider.LogErrorMessage("Exception occured during data operation.", exception);
                throw;
            }
        }

        public void Dispose()
        {
            connection?.Dispose();
            connection = null;
        }
    }
}
