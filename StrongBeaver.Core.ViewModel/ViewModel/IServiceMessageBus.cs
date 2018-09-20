using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public interface IViewModelMessageBus
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IViewModelMessage;

        void Send<TMessage, TTarget>(TMessage message)
            where TMessage : IViewModelMessage
            where TTarget : IMessageBusRecipient<TMessage>;

        void Send<TMessage>(TMessage message, object token)
            where TMessage : IViewModelMessage;
    }
}
