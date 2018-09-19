using System;
using System.Threading.Tasks;

namespace StrongBeaver.Services.Storage.Data
{
    public interface IDataBuilder : IDisposable
    {
        Task BuildStorageAsync();

        Task DeleteStorageAsync();
    }
}
