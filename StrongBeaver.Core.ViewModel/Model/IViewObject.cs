using System;

namespace StrongBeaver.Core.Model
{
    public interface IViewObject<in TItem> : IInitialisable, IUpdateable<TItem>, IDisposable
    {
        // No member
    }
}