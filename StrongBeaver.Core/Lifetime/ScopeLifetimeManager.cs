using System;

namespace StrongBeaver.Core.Lifetime
{
    public class ScopeLifetimeManager : ILifetimeManager, IDisposable
    {
        public ScopeLifetimeManager()
        {
            IsAlive = true;
        }

        public bool IsAlive { get; private set; }

        public void Dispose()
        {
            IsAlive = false;
        }
    }
}
