namespace StrongBeaver.Core.Services.Dialog
{
    public class ToastMessage : ServiceMessage, IDialogMessage
    {
        public ToastMessage()
        {
            // No operation
        }

        public ToastMessage(object sender)
            : base(sender)
        {
            // No operation
        }

        public ToastMessage(IToast toast)
        {
            Toast = toast;
        }

        public IToast Toast { get; set; }

        public void PerformMessage(IDialogService service)
        {
            service.ShowToast(Toast);
        }
    }
}
