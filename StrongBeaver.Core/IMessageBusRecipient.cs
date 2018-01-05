namespace StrongBeaver.Core
{
    public interface IMessageBusRecipient<in TMessage>
    {
        void ProcessMessage(TMessage message);
    }
}
