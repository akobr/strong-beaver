using StrongBeaver.Core.Container;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core
{
    public class ProviderWithMessageBus<TItem> : Provider<TItem>, IMessageBusProvider
        where TItem : class
    {
        private readonly IMessenger messenger;

        public ProviderWithMessageBus(IContainer container)
            : base(container)
        {
            messenger = new Messenger();
        }

        public IMessenger Messanger => messenger;

        public void RegisterToMessageBus<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
        {
            if (recipient == null)
            {
                return;
            }

            messenger.Register<TMessage>(recipient, recipient.ProcessMessage);
            Provider.LogDebugMessage($"The message bus recipient '{typeof(TRecipient).Name}' has been registered with message type '{typeof(TMessage).Name}'.");
        }

        public void UnregisterFromMessageBus(object recipient)
        {
            messenger.Unregister(recipient);
        }
    }
}
