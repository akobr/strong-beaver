using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public class ViewModelMessageBus : IViewModelMessageBus, IViewModelMessageBusRegister
    {
        private readonly GenericMessageBus messenger;

        public ViewModelMessageBus()
        {
            messenger = new GenericMessageBus();
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
            where TTarget : IMessageBusViewModel<TMessage>
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

        public void Register<TRecipient, TMessage>(IMessageBusViewModel<TMessage> recipient)
            where TMessage : IViewModelMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, true, recipient.ProcessMessage, false);
        }

        public void Register<TRecipient, TMessage>(IMessageBusViewModel<TMessage> recipient, object token)
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

        public void Unregister<TMessage>(IMessageBusViewModel<TMessage> recipient)
            where TMessage : IViewModelMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient);
        }

        public void Unregister<TMessage>(IMessageBusViewModel<TMessage> recipient, object token)
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