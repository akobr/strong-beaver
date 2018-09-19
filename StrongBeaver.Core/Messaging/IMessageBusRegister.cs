namespace StrongBeaver.Core.Messaging
{
    public interface IMessageBusRegister
    {
        void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IMessage;

        void Register<TRecipient, TMessage>(TRecipient recipient, object token)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IMessage;

        void Unregister(IMessageBusRecipient recipient);

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IMessage;

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IMessage;

    }
}