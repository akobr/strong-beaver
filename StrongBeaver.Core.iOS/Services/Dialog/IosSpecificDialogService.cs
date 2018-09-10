using System;
using System.Threading.Tasks;
using UIKit;
using System.Collections.Generic;
using CommonServiceLocator;
using CoreGraphics;
using StrongBeaver.Core.Platform;

namespace StrongBeaver.Core.Services.Dialog
{
    class IosSpecificDialogService : IPlatformSpecificDialogService
    {
        private readonly ToastManager toastManager;
        public IosSpecificDialogService()
        {
            toastManager = new ToastManager();
        }

        private UIViewController ViewController => UIApplication.SharedApplication.KeyWindow.RootViewController;

        public Task ShowMessage(string message, string title)
        {
            return ShowMessage(message, title, "OK", null);
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create(buttonText, UIAlertActionStyle.Default,
                (alertAction) =>
                {
                    afterHideCallback?.Invoke();
                    tcs.SetResult(true);
                }));

            ViewController.PresentViewController(alert, true, null);

            return tcs.Task;
        }

        public Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions)
        {
            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            int actionIndex = -1;

            UIAlertController alert = UIAlertController.Create(
                title,
                message,
                actions.Count > 2
                    ? UIAlertControllerStyle.ActionSheet
                    : UIAlertControllerStyle.Alert);

            foreach (IDialogActionDescription action in actions)
            {
                int currentIndex = ++actionIndex;

                alert.AddAction(
                UIAlertAction.Create(
                    action.Title ?? "[NoText]",
                    GetButtonStyle(action.Type),
                    (alertAction) =>
                    {
                        action.Action?.Invoke();
                        tcs.SetResult(currentIndex);
                    }));
            }

            // TODO: [P2] This must be solved properly!
            if (alert.PopoverPresentationController != null
                && IsTablet())
            {
                alert.PopoverPresentationController.SourceView = ViewController.View;
                alert.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Unknown;
                alert.PopoverPresentationController.SourceRect = new CGRect(0.0, 0.0, 1.0, 1.0);
            }

            ViewController.PresentViewController(alert, true, null);
            return tcs.Task;
        }

        public void ShowToast(IToast toast)
        {
            toastManager.ShowMessage(toast.Message, toast.Duration, toast.Action);
        }

        private UIAlertActionStyle GetButtonStyle(DialogActionTypeEnum actionType)
        {
            switch (actionType)
            {
                case DialogActionTypeEnum.Destructive:
                    return UIAlertActionStyle.Destructive;

                case DialogActionTypeEnum.Negative:
                    return UIAlertActionStyle.Cancel;

                default:
                    return UIAlertActionStyle.Default;
            }
        }

        private bool IsTablet()
        {
            try
            {
                return ServiceLocator.Current.GetInstance<IDeviceInfo>().FamilyType == DeviceFamilyTypeEnum.Tablet;
            }
            catch
            {
                return false;
            }
        }
    }
}
