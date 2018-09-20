using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Dialog
{
    public class NamedDialogMessage : ServiceAsyncMesssage, IDialogMessage
    {
        public NamedDialogMessage()
        {
            // No operation
        }

        public NamedDialogMessage(string dialogName)
        {
            DialogName = dialogName;
        }

        public string DialogName { get; set; }

        public void PerformMessage(IDialogService service)
        {
            OperationHolder = new TaskHolder(service.ShowDialogAsync(DialogName));
        }
    }
}