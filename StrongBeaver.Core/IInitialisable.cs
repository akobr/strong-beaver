namespace StrongBeaver.Core
{
    public interface IInitialisable
    {
        void Initialise();
    }

    public interface IInitialisable<in TContext>
    {
        void Initialise(TContext context);
    }
}