using Microsoft.EntityFrameworkCore;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public abstract class BaseDataContext : DbContext, IDataContext
    {
        protected BaseDataContext()
            : base()
        {
            // No operation
        }

        protected BaseDataContext(DbContextOptions options)
            : base(options)
        {
            // No operation
        }
    }
}
