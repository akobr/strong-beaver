using System;

namespace StrongBeaver.Services.Storage.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}