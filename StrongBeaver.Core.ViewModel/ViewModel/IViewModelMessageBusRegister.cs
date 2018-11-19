using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public interface IViewModelMessageBusRegister
    {
        void Register<TRecipient, TMessage>(IMessageBusViewModel<TMessage> recipient)
            where TMessage : IViewModelMessage;

        void Register<TRecipient, TMessage>(IMessageBusViewModel<TMessage> recipient, object token)
            where TMessage : IViewModelMessage;

        void Unregister(IMessageBusRecipient recipient);

        void Unregister<TMessage>(IMessageBusViewModel<TMessage> recipient)
            where TMessage : IViewModelMessage;

        void Unregister<TMessage>(IMessageBusViewModel<TMessage> recipient, object token)
            where TMessage : IViewModelMessage;

    }
}