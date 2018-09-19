using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Dialog
{
    public class SimpleDialogMessage : ServiceAsyncMesssage, IDialogMessage
    {
        public SimpleDialogMessage()
        {
            // No operation
        }

        public SimpleDialogMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public void PerformMessage(IDialogService service)
        {
            OperationHolder = new TaskHolder(service.ShowDialogAsync(Title, Message));
        }
    }
}