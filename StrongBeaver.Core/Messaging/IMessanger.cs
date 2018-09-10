namespace StrongBeaver.Core.Messaging
{
    public interface IMessenger : GalaSoft.MvvmLight.Messaging.IMessenger
    {
        void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>;
    }
}