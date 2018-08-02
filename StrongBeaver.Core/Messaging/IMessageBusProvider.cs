namespace StrongBeaver.Core.Messaging
{
    public interface IMessageBusProvider
    {
        IMessenger Messanger { get; }

        void RegisterToMessageBus<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>;

        void UnregisterFromMessageBus(object recipient);
    }
}
