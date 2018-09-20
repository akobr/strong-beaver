namespace StrongBeaver.Core.Startup
{
    public abstract class BaseActivator : IActivator
    {
        public void Startup()
        {
            Register();
            Resolve();
            Release();
        }

        public abstract void Register();

        public abstract void Resolve();

        public void Release()
        {
            OnRelease();
        }

        protected virtual void OnRelease()
        {
            // no operation ( template method )
        }
    }
}
