namespace StrongBeaver.Core.Messaging
{
    public interface IMessageBusRecipient
    {
        // no operation
    }

    public interface IMessageBusRecipient<in TMessage> : IMessageBusRecipient
        where TMessage : IMessage
    {
        void ProcessMessage(TMessage message);
    }
}
