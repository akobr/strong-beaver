using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StrongBeaver.Core.Services.Storage.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public class DefaultDataOperation : IDataOperation
    {
        private readonly IDataContext context;

        public DefaultDataOperation(IDataContext context)
        {
            this.context = context;
        }

        public void StartTransaction()
        {
            if (context.Database.CurrentTransaction != null)
            {
                return;
            }

            context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            context.Database.RollbackTransaction();
        }

        public TData Get<TData>(object primaryKey)
            where TData : class
        {
            return context.Set<TData>().Find(primaryKey);
        }

        public TData Get<TData>(Expression<Func<TData, bool>> predicate)
            where TData : class
        {
            return context.Set<TData>().FirstOrDefault(predicate);
        }

        public IEnumerable<TData> GetAll<TData>()
            where TData : class
        {
            return GetAllQueryable<TData>();
        }

        public IQueryable<TData> GetAllQueryable<TData>()
            where TData : class
        {
            return context.Set<TData>();
        }

        public void Store<TData>(TData item)
            where TData : class
        {
            if (item == null)
            {
                return;
            }

            DbSet<TData> entries = context.Set<TData>();
            EntityEntry<TData> entry = context.Entry<TData>(item);

            if (entry.State == EntityState.Detached)
            {
                entry = entries.Attach(item);

                if (entry.State == EntityState.Unchanged)
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        public void Store<TData>(IEnumerable<TData> items)
            where TData : class
        {
            foreach (TData item in items)
            {
                Store<TData>(item);
            }
        }

        public bool Delete<TData>(TData item)
            where TData : class
        {
            if (item == null)
            {
                return false;
            }

            DbSet<TData> entries = context.Set<TData>();
            EntityEntry<TData> entry = context.Entry<TData>(item);

            if (entry.State == EntityState.Detached)
            {
                entry = entries.Attach(item);
            }

            entry = entries.Remove(item);
            return entry.State == EntityState.Deleted;
        }

        public bool Delete<TData>(object primaryKey)
            where TData : class
        {
            TData item = Get<TData>(primaryKey);
            return Delete<TData>(item);
        }

        public int DeleteAll<TData>()
            where TData : class
        {
            int result = 0;

            foreach (TData item in GetAll<TData>())
            {
                if(Delete<TData>(item))
                {
                    result++;
                }
            }

            return result;
        }

        public int DeleteAll<TData>(IEnumerable<TData> items)
            where TData : class
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
            where TData : class
        {
            int result = 0;

            foreach (TData item in GetAllQueryable<TData>().Where(predicate))
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
                context.SaveChanges();
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
