using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public class ServiceMessageBus : IServiceMessageBus, IServiceMessageBusRegister
    {
        private readonly GenericMessageBus messenger;

        public ServiceMessageBus()
        {
            messenger = new GenericMessageBus();
        }

        public void Send<TMessage>(TMessage message)
            where TMessage : IServiceMessage
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage>(message);
        }

        public void Send<TMessage, TTarget>(TMessage message)
            where TMessage : IServiceMessage
            where TTarget : IMessageBusService<TMessage>
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage, TTarget>(message);
        }

        public void Send<TMessage>(TMessage message, object token)
            where TMessage : IServiceMessage
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage>(message, token);
        }

        public void Register<TMessage>(IMessageBusService<TMessage> recipient)
            where TMessage : IServiceMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, true, recipient.ProcessMessage, false);
        }

        public void Register<TMessage>(IMessageBusService<TMessage> recipient, object token)
            where TMessage : IServiceMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, token, true, recipient.ProcessMessage, false);
        }

        public void Unregister(IMessageBusRecipient recipient)
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister(recipient);
        }

        public void Unregister<TMessage>(IMessageBusService<TMessage> recipient)
            where TMessage : IServiceMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient);
        }

        public void Unregister<TMessage>(IMessageBusService<TMessage> recipient, object token)
            where TMessage : IServiceMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient, token, recipient.ProcessMessage);
        }
    }
}