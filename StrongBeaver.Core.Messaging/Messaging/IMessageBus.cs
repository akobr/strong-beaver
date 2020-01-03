namespace StrongBeaver.Core.Messaging
{
    public interface IMessageBus
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;

        void Send<TMessage, TTarget>(TMessage message)
            where TMessage : IMessage
            where TTarget : IMessageBusRecipient<TMessage>;

        void Send<TMessage>(TMessage message, object token)
            where TMessage : IMessage;
    }
}