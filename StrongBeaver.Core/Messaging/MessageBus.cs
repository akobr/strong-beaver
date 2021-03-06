﻿namespace StrongBeaver.Core.Messaging
{
    public class MessageBus : IMessageBus, IMessageBusRegister
    {
        private readonly GenericMessageBus messenger;

        public MessageBus()
        {
            messenger = new GenericMessageBus();
        }

        public void Send<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage>(message);
        }

        public void Send<TMessage, TTarget>(TMessage message)
            where TMessage : IMessage
            where TTarget : IMessageBusRecipient<TMessage>
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage, TTarget>(message);
        }

        public void Send<TMessage>(TMessage message, object token)
            where TMessage : IMessage
        {
            if (message == null)
            {
                return;
            }

            messenger.Send<TMessage>(message, token);
        }

        public void Register<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, true, recipient.ProcessMessage, false);
        }

        public void Register<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IMessage
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
            where TMessage : IMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient);
        }

        public void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IMessage
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Unregister<TMessage>(recipient, token, recipient.ProcessMessage);
        }
    }
}