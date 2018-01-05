using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;

namespace StrongBeaver.Core.Services.Storage.Data.Extensions
{
    public static class DbUpdateExceptionExtensions
    {
        public static string CreateInvalidEntriesMessage(this DbUpdateException exception)
        {
            StringBuilder stb = new StringBuilder();

            foreach (EntityEntry entry in exception.Entries)
            {
                stb.AppendLine($"Entity of type '{entry.Entity.GetType().Name}' in state '{entry.State}' has update error.");
            }

            return stb.ToString();
        }
    }
}
