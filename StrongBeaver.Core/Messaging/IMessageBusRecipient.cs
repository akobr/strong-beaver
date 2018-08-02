namespace StrongBeaver.Core.Messaging
{
    public interface IMessageBusRecipient<in TMessage>
    {
        void ProcessMessage(TMessage message);
    }
}
