using StrongBeaver.Core.Container;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public class ViewModelProvider : ProviderWithMessageBus<IViewModel>, IViewModelProvider
    {
        public ViewModelProvider(IContainer container)
            : base(container, new Messenger())
        {
            Provider.LogDebugMessage($"The ViewModel provider '{GetType().Name}' has been created.");
        }

        public static IViewModelProvider Current { get; private set; }

        public static void SetCurrentProvider(IViewModelProvider viewModelProvider)
        {
            Current = viewModelProvider;
        }
    }
}