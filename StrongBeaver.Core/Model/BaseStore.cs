using System;

namespace StrongBeaver.Core.Model
{
    public abstract class BaseStore : IStore
    {
        public void Cleanup()
        {
            OnCleanup();
        }

        public void Dispose()
        {
            OnDispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnCleanup()
        {
            // No operation ( temlate method )
        }

        protected virtual void OnDispose(bool disposing)
        {
            // No operation ( template method )
        }
    }
}