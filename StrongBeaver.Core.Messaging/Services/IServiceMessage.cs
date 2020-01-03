using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public interface IServiceMessage : IMessage
    {
        // No member
    }

    public interface IServiceMessage<in TService> : IServiceMessage
        where TService : IService
    {
        void PerformMessage(TService service);
    }
}