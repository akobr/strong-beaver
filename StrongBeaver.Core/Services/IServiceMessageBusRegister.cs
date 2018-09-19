using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public interface IServiceMessageBusRegister
    {
        void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IServiceMessage;

        void Register<TRecipient, TMessage>(TRecipient recipient, object token)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IServiceMessage;

        void Unregister(IMessageBusRecipient recipient);

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IServiceMessage;

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IServiceMessage;

    }
}