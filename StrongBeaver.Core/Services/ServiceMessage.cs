using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public class ServiceMessage : Message, IServiceMessage
    {
        public ServiceMessage()
            : base()
        {
            // No operation
        }

        protected ServiceMessage(object sender)
            : base(sender)
        {
            // No operation
        }

        protected ServiceMessage(object sender, object target)
            : base(sender, target)
        {
            // No operation
        }

        public ServiceMessage(object sender, string code)
            : base(sender, code)
        {
            // No operation
        }
    }

    public class ServiceMessage<T> : ServiceMessage
    {
        public ServiceMessage()
            : base()
        {
            // No operation
        }

        protected ServiceMessage(object sender)
            : base(sender)
        {
            // No operation
        }

        protected ServiceMessage(object sender, object target)
            : base(sender, target)
        {
            // No operation
        }

        public ServiceMessage(object sender, string code)
            : base(sender, code)
        {
            // No operation
        }

        public T Content { get; protected set; }
    }
}