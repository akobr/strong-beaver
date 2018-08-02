namespace StrongBeaver.Core.Lifetime
{
    public class ReferenceCountLifetimeManager : ILifetimeManager
    {
        private int references;

        public ReferenceCountLifetimeManager()
        {
            references = 1;
        }

        public bool IsAlive => references > 0;

        public void IncreaseReferences()
        {
            ++references;
        }

        public void DescreseReferences()
        {
            --references;
        }
    }
}
