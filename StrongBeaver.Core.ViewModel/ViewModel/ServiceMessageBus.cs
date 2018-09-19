using GalaSoft.MvvmLight.Messaging;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public class ViewModelMessageBus : IViewModelMessageBus, IViewModelMessageBusRegister
    {
        private readonly Messenger messenger;

        public ViewModelMessageBus()
        {
            messenger = new Messenger();
        }

        public void Send<TMessage>(TMessage message)
            where TMessage : IViewModelMessage
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage>(message);
        }

        public void Send<TMessage, TTarget>(TMessage message)
            where TMessage : IViewModelMessage
            where TTarget : IMessageBusRecipient<TMessage>
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage, TTarget>(message);
        }

        public void Send<TMessage>(TMessage message, object token)
            where TMessage : IViewModelMessage
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage>(message, token);
        }

        public void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IViewModelMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, true, recipient.ProcessMessage, false);
        }

        public void Register<TRecipient, TMessage>(TRecipient recipient, object token)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IViewModelMessage
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
            where TMessage : IViewModelMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient);
        }

        public void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IViewModelMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient, token, recipient.ProcessMessage);
        }
    }
}