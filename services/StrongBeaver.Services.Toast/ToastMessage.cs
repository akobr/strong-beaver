using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Toast
{
    public class ToastMessage : ServiceMessage, IToastMessage
    {
        public ToastMessage()
        {
            // No operation
        }

        public ToastMessage(IToast toast)
        {
            Toast = toast;
        }

        public IToast Toast { get; set; }

        public void PerformMessage(IToastService service)
        {
            service.ShowToast(Toast);
        }
    }
}
