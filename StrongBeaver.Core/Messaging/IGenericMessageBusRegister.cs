using System;

namespace StrongBeaver.Core.Messaging
{
    public interface IGenericMessageBusRegister
    {
        void Register<TMessage>(object recipient, Action<TMessage> action, bool keepTargetAlive = false);

        void Register<TMessage>(object recipient, object token, Action<TMessage> action, bool keepTargetAlive = false);

        void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false);

        void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false);

        void Unregister(object recipient);

        void Unregister<TMessage>(object recipient);

        void Unregister<TMessage>(object recipient, object token);

        void Unregister<TMessage>(object recipient, Action<TMessage> action);

        void Unregister<TMessage>(object recipient, object token, Action<TMessage> action);
    }
}