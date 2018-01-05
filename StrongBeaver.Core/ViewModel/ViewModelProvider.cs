using StrongBeaver.Core.Platform;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Ioc;

namespace StrongBeaver.Core.ViewModel
{
    public class ViewModelProvider : ProviderWithMessageBus<IViewModel>, IViewModelProvider
    {
        private readonly IMessenger messenger;

        public ViewModelProvider(ISimpleIoc container, IPlatformInfo platformInfo)
            : base(container)
        {
            messenger = GalaSoft.MvvmLight.Messaging.Messenger.Default;

            Provider.LogDebugMessage($"The ViewModel provider '{GetType().Name}' has been created.");
        }

        public IMessenger Messenger => messenger;

        public static IViewModelProvider Current { get; private set; }

        public static void SetCurrentProvider(IViewModelProvider viewModelProvider)
        {
            Current = viewModelProvider;
        }
    }
}