namespace StrongBeaver.Core.Services
{
    public interface IMessageBusService<in TMessage> : IMessageBusRecipient<TMessage>
        where TMessage : IServiceMessage
    {
        // No members
    }
}