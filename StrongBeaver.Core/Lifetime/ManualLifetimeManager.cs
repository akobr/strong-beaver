namespace StrongBeaver.Core.Lifetime
{
    public class ManualLifetimeManager : ILifetimeManager
    {
        public ManualLifetimeManager(bool isAlive)
        {
            IsAlive = isAlive;
        }

        public ManualLifetimeManager()
        {
            IsAlive = false;
        }

        public bool IsAlive { get; private set; }

        public void SetAlive()
        {
            IsAlive = true;
        }

        public void SetDead()
        {
            IsAlive = false;
        }
    }
}
