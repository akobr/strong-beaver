using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public interface IMessageBusViewModel<in TMessage> : IViewModel, IMessageBusRecipient<TMessage>
        where TMessage : IViewModelMessage
    {
        // no members
    }
}
