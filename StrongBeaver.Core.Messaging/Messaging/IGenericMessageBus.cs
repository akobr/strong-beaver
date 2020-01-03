namespace StrongBeaver.Core.Messaging
{
    public interface IGenericMessageBus
    {
        void Send<TMessage>(TMessage message);

        void Send<TMessage, TTarget>(TMessage message);

        void Send<TMessage>(TMessage message, object token);
    }
}