﻿using StrongBeaver.Core.Container;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core
{
    public class ProviderWithMessageBus<TItem> : Provider<TItem>, IMessageBusProvider
        where TItem : class
    {
        public ProviderWithMessageBus(IContainer container)
            : base(container)
        {
            Messanger = new Messenger();
        }

        public ProviderWithMessageBus()
            : this(new SimpleIocContainer())
        {
            // no operation
        }

        public IMessenger Messanger { get; }

        public void RegisterToMessageBus<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
        {
            if (recipient == null)
            {
                return;
            }

            Messanger.Register<TRecipient, TMessage>(recipient);
            Provider.LogDebugMessage($"The message bus recipient '{typeof(TRecipient).Name}' has been registered with message type '{typeof(TMessage).Name}'.");
        }

        public void UnregisterFromMessageBus(object recipient)
        {
            Messanger.Unregister(recipient);
        }
    }
}
