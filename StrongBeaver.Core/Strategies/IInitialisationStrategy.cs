namespace StrongBeaver.Core.Strategies
{
    public interface IInitialisationStrategy<in TItem>
    {
        void InitialiseItem(TItem item);
    }
}
