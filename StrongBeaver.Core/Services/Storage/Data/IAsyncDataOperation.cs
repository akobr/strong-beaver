using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public interface IAsyncDataOperation : IDisposable
    {
        Task StartTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();


        Task<TData> GetAsync<TData>(object primaryKey)
            where TData : class;

        Task<TData> GetAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class;

        Task<IList<TData>> GetAllAsync<TData>()
            where TData : class;

        Task<IList<TData>> GetAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class;

        Task StoreAsync<TData>(TData item)
            where TData : class;

        Task StoreAsync<TData>(IEnumerable<TData> items)
            where TData : class;

        Task<bool> DeleteAsync<TData>(object primaryKey)
            where TData : class;

        Task<bool> DeleteAsync<TData>(TData item)
            where TData : class;

        Task<int> DeleteAllAsync<TData>()
            where TData : class;

        Task<int> DeleteAllAsync<TData>(IEnumerable<TData> items)
            where TData : class;

        Task<int> DeleteAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class;

        Task SaveAsync();
    }
}