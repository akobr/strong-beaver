namespace StrongBeaver.Core.Strategies
{
    public interface IUpdateStrategy<in TItem>
    {
        void UpdateIt(TItem updateItem, TItem byItem);
    }
}
