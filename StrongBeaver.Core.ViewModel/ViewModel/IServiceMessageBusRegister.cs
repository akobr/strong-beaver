using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public interface IViewModelMessageBusRegister
    {
        void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IViewModelMessage;

        void Register<TRecipient, TMessage>(TRecipient recipient, object token)
            where TRecipient : class, IMessageBusRecipient<TMessage>
            where TMessage : IViewModelMessage;

        void Unregister(IMessageBusRecipient recipient);

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient)
            where TMessage : IViewModelMessage;

        void Unregister<TMessage>(IMessageBusRecipient<TMessage> recipient, object token)
            where TMessage : IViewModelMessage;

    }
}