namespace StrongBeaver.Core.Strategies
{
    public interface IDisposableStrategy<in TItem>
    {
        void DisposeIt(TItem item);
    }
}
