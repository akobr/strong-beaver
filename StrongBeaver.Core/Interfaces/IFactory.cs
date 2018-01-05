namespace StrongBeaver.Core
{
    public interface IFactory<out TCreation>
    {
        TCreation Create();
    }

    public interface IFactory<out TCreation, in TContext>
    {
        TCreation Create(TContext context);
    }
}
