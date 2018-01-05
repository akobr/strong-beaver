using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Dialog
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
            service.ShowDialogByNameAsync(DialogName);
        }
    }
}