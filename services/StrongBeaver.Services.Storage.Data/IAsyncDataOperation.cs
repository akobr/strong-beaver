using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StrongBeaver.Services.Storage.Data
{
    public interface IAsyncDataOperation : IDisposable
    {
        IAsyncTransaction Transaction { get; }

        Task<TData> GetAsync<TData>(object primaryKey)
            where TData : new();

        Task<TData> GetAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new();

        Task<IList<TData>> GetAllAsync<TData>()
            where TData : new();

        Task<IList<TData>> GetAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new();

        Task StoreAsync<TData>(TData item)
            where TData : new();

        Task StoreAsync<TData>(IEnumerable<TData> items)
            where TData : new();

        Task<bool> DeleteAsync<TData>(object primaryKey)
            where TData : new();

        Task<bool> DeleteAsync<TData>(TData item)
            where TData : new();

        Task<int> DeleteAllAsync<TData>()
            where TData : new();

        Task<int> DeleteAllAsync<TData>(IEnumerable<TData> items)
            where TData : new();

        Task<int> DeleteAllAsync<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new();

        Task SaveAsync();
    }
}