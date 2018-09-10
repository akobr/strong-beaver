using System;

namespace StrongBeaver.Core.Services
{
    public abstract class BaseService : IService
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
