using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Dialog
{
    public class SimpleDialogMessage : ServiceAsyncMesssage, IDialogMessage
    {
        public SimpleDialogMessage()
        {
            // No operation
        }

        public SimpleDialogMessage(object sender)
            : base(sender)
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
            TaskHolder result = new TaskHolder
            {
                Operation = service.ShowMessage(Title, Message)
            };

            OperationHolder = result;
        }
    }
}