using System;
using System.Threading.Tasks;

namespace StrongBeaver.Services.Storage.Data
{
    public interface IAsyncTransaction : IDisposable
    {
        Task CommitAsync();

        Task RollbackAsync();
    }
}