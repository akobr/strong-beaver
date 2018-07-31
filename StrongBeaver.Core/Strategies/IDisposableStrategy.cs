namespace StrongBeaver.Core.Strategies
{
    public interface IDisposableStrategy<in TItem>
    {
        void DisposeItem(TItem item);
    }
}
