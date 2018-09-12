using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public interface IViewModelProvider : IProvider<IViewModel>, IMessageBusProvider
    {
        // No member
    }
}