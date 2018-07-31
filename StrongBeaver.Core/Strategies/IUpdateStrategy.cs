namespace StrongBeaver.Core.Strategies
{
    public interface IUpdateStrategy<in TItem>
    {
        void UpdateItem(TItem updateItem, TItem byItem);
    }
}
