using StrongBeaver.Core.Services;

namespace StrongBeaver.Showroom.Examples
{
    public interface INewServiceMessage : IServiceMessage
    {
        // Specify the members
        void PerformMessage(INewMessageBusService service);
    }

    public interface INewMessageBusService : IMessageBusService<INewServiceMessage>
    {
        // Implement your service interface
    }

    public class NewMessageBusService : INewMessageBusService
    {
        // Implement your service

        public void ProcessMessage(INewServiceMessage message)
        {
            message.PerformMessage(this);
        }
    }
}
