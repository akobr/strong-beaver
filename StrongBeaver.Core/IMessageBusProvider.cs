using GalaSoft.MvvmLight.Messaging;

namespace StrongBeaver.Core
{
    public interface IMessageBusProvider
    {
        IMessenger Messanger { get; }

        void RegisterToMessageBus<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>;

        void UnregisterFromMessageBus(object recipient);
    }
}
