using GalaSoft.MvvmLight.Messaging;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public class ServiceMessageBus : IServiceMessageBus, IServiceMessageBusRegister
    {
        private readonly Messenger messenger;

        public ServiceMessageBus()
        {
            messenger = new Messenger();
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
            where TTarget : IMessageBusRecipient<TMessage>
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

        public void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IServiceMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, true, recipient.ProcessMessage, false);
        }

        public void Register<TRecipient, TMessage>(TRecipient recipient, object token)
            where TRecipient : class, IMessageBusRecipient<TMessage>
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

        public void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IServiceMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient);
        }

        public void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
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