namespace StrongBeaver.Core
{
    public interface IUpdateable<in TNewValue>
    {
        void Update(TNewValue newValue);
    }
}