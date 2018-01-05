namespace StrongBeaver.Core.Services.Dialog
{
    public interface IDialogMessage : IServiceMessage
    {
        void PerformMessage(IDialogService service);
    }
}