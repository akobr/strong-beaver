using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public interface IMessageBusService<in TMessage> : IService, IMessageBusRecipient<TMessage>
        where TMessage : IServiceMessage
    {
        // No members
    }
}