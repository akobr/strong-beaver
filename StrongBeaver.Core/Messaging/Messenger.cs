namespace StrongBeaver.Core.Messaging
{
    public class Messenger : GalaSoft.MvvmLight.Messaging.Messenger, IMessenger
    {
        public void Register<TRecipient, TMessage>(TRecipient recipient)
            where TRecipient : class, IMessageBusRecipient<TMessage>
        {
            if (recipient == null)
            {
                return;
            }

            Register<TMessage>(recipient, recipient.ProcessMessage);
        }
    }
}
