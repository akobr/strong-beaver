using Microsoft.EntityFrameworkCore;
using StrongBeaver.Core.Services.Storage.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class DefaultAsyncDataOperation : IAsyncDataOperation
    {
        private readonly IDataContext context;

        public DefaultAsyncDataOperation(IDataContext context)
        {
            this.context = context;
        }

        public Task StartTransactionAsync()
        {
            return context.Database.BeginTransactionAsync();
        }

        public Task CommitTransactionAsync()
        {
            return Task.Run(() => context.Database.CommitTransaction());
        }

        public Task RollbackTransactionAsync()
        {
            return Task.Run(() => context.Database.RollbackTransaction());
        }

        public async Task<IList<TData>> GetAllAsync<TData>()
            where TData : class
        {
            return await context.Set<TData>().ToListAsync();
        }

        public async Task<IList<TData>> GetAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class
        {
            return await context.Set<TData>().Where<TData>(predicate).ToListAsync();
        }

        public Task<TData> GetAsync<TData>(object primaryKey)
            where TData : class
        {
            return context.Set<TData>().FindAsync(primaryKey);
        }

        public Task<TData> GetAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class
        {
            return context.Set<TData>().FirstOrDefaultAsync(predicate);
        }

        public Task StoreAsync<TData>(TData item)
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).Store<TData>(item));
        }

        public Task StoreAsync<TData>(IEnumerable<TData> items)
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).Store<TData>(items));
        }

        public Task<int> DeleteAllAsync<TData>()
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).DeleteAll<TData>());
        }

        public Task<int> DeleteAllAsync<TData>(IEnumerable<TData> items)
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).DeleteAll<TData>(items));
        }

        public Task<int> DeleteAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).DeleteAll<TData>(predicate));
        }

        public Task<bool> DeleteAsync<TData>(object primaryKey)
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).Delete<TData>(primaryKey));
        }

        public Task<bool> DeleteAsync<TData>(TData item)
            where TData : class
        {
            return Task.Run(() => new DefaultDataOperation(context).Delete<TData>(item));
        }

        public Task SaveAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                Provider.LogErrorMessage("Update exception occured during data operation.", exception.CreateInvalidEntriesMessage(), exception);
                throw;
            }
            catch (Exception exception)
            {
                Provider.LogErrorMessage("Exception occured during data operation.", exception);
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
