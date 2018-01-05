namespace StrongBeaver.Core.Services.Navigation
{
    public interface INavigationMessage : IServiceMessage
    {
        void PerformMessage(INavigationService service);
    }
}