using System;

namespace StrongBeaver.Core.Model
{
    public interface IComplexStoreItem<in TItem> : IInitialisable, IUpdateable<TItem>, IDisposable
    {
        // No member
    }
}