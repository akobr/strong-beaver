namespace StrongBeaver.Core.ViewModel
{
    public interface IMessageBusViewModel<in TMessage> : IViewModel
        where TMessage : IViewModelMessage
    {
        void ProcessMessage(TMessage message);
    }
}
