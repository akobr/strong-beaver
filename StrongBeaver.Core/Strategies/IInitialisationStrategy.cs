namespace StrongBeaver.Core.Strategies
{
    public interface IInitialisationStrategy<in TItem>
    {
        void InitialiseIt(TItem item);
    }
}
