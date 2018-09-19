using UIKit;

namespace StrongBeaver.Services.Toast
{
    public class IosToastService : IToastService
    {
        private readonly ToastManager toastManager;

        public IosToastService()
        {
            toastManager = new ToastManager();
        }

        private UIViewController ViewController => UIApplication.SharedApplication.KeyWindow.RootViewController;

        public void ShowToast(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            ShowToast(new Toast(text));
        }

        public void ShowToast(IToast toast)
        {
            toastManager.ShowMessage(toast.Message, toast.Duration, toast.Action);
        }
    }
}
