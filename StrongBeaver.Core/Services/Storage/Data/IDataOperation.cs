using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public interface IDataOperation : IDisposable
    {
        void StartTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        TData Get<TData>(object primaryKey)
            where TData : new();

        TData Get<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new();

        IEnumerable<TData> GetAll<TData>()
            where TData : new();

        //IEnumerable<TData> GetAll<TData>(Expression<Func<TData, bool>> predicate)
        //    where TData : class;

        void Store<TData>(TData item)
            where TData : new();

        void Store<TData>(IEnumerable<TData> items)
            where TData : new();

        bool Delete<TData>(object primaryKey)
            where TData : new();

        bool Delete<TData>(TData item)
            where TData : new();

        int DeleteAll<TData>()
            where TData : new();

        int DeleteAll<TData>(IEnumerable<TData> items)
            where TData : new();

        int DeleteAll<TData>(Expression<Func<TData, bool>> predicate)
            where TData : new();

        void Save();

        Task SaveAsync();
    }
}