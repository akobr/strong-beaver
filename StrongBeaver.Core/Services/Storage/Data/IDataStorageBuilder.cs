using System;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public interface IDataStorageBuilder : IDisposable
    {
        void EnsureModel();

        Task EnsureModelAsync();

        void EnsureDeleted();

        Task EnsureDeletedAsync();
    }
}
