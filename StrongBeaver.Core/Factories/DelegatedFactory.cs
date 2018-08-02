using System;
namespace StrongBeaver.Core.Helpers
{
    public class DelegatedFactory<TCreation> : IFactory<TCreation>
    {
        private readonly Func<TCreation> createDelegate;

        public DelegatedFactory(Func<TCreation> createDelegate)
        {
            this.createDelegate = createDelegate;
        }

        public TCreation Create()
        {
            return createDelegate();
        }
    }

    public class DelegatedFactory<TCreation, TContext> : IFactory<TCreation, TContext>
    {
        private readonly Func<TContext, TCreation> createDelegate;

        public DelegatedFactory(Func<TContext, TCreation> createDelegate)
        {
            this.createDelegate = createDelegate;
        }

        public TCreation Create(TContext context)
        {
            return createDelegate(context);
        }
    }
}
