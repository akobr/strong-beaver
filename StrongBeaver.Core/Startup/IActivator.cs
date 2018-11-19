namespace StrongBeaver.Core.Startup
{
    public interface IActivator
    {
        void Register();

        void Resolve();

        void Release();
    }
}