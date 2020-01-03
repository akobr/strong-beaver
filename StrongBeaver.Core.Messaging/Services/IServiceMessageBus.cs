namespace StrongBeaver.Core.Services
{
    public interface IServiceMessageBus
    {
        void Send<TMessage>(TMessage message)
            where TMessage : IServiceMessage;

        void Send<TMessage, TTarget>(TMessage message)
            where TMessage : IServiceMessage
            where TTarget : IMessageBusService<TMessage>;

        void Send<TMessage>(TMessage message, object token)
            where TMessage : IServiceMessage;
    }
}
