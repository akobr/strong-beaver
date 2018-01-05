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
            where TData : class;

        TData Get<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class;

        IEnumerable<TData> GetAll<TData>()
            where TData : class;

        //IEnumerable<TData> GetAll<TData>(Expression<Func<TData, bool>> predicate)
        //    where TData : class;

        void Store<TData>(TData item)
            where TData : class;

        void Store<TData>(IEnumerable<TData> items)
            where TData : class;

        bool Delete<TData>(object primaryKey)
            where TData : class;

        bool Delete<TData>(TData item)
            where TData : class;

        int DeleteAll<TData>()
            where TData : class;

        int DeleteAll<TData>(IEnumerable<TData> items)
            where TData : class;

        int DeleteAll<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class;

        void Save();

        Task SaveAsync();
    }
}