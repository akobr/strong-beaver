using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public interface IServiceMessageBusRegister
    {
        void Register<TMessage>(IMessageBusService<TMessage> recipient)
            where TMessage : IServiceMessage;

        void Register<TMessage>(IMessageBusService<TMessage> recipient, object token)
            where TMessage : IServiceMessage;

        void Unregister(IMessageBusRecipient recipient);

        void Unregister<TMessage>(IMessageBusService<TMessage> recipient)
            where TMessage : IServiceMessage;

        void Unregister<TMessage>(IMessageBusService<TMessage> recipient, object token)
            where TMessage : IServiceMessage;

    }
}