using System;

namespace StrongBeaver.Core.Services
{
    public abstract class BaseDisposableService : IService, IDisposable
    {
        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose(bool disposing)
        {
            // No operation ( template method )
        }
    }
}
