namespace StrongBeaver.Core.Messaging
{
    public interface IMessageBusRegister
    {
        void Register<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IMessage;

        void Register<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IMessage;

        void Unregister(IMessageBusRecipient recipient);

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IMessage;

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IMessage;

    }
}