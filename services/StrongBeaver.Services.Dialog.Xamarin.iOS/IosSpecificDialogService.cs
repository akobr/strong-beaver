using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreGraphics;
using StrongBeaver.Core.Platform;
using UIKit;

namespace StrongBeaver.Services.Dialog
{
    public class IosSpecificDialogService : IPlatformSpecificDialogService
    {
        private readonly IDeviceInfo deviceInfo;

        public IosSpecificDialogService(IPlatformProvider platformProvider)
        {
            deviceInfo = platformProvider.DeviceInfo;
        }

        private static UIViewController ViewController => UIApplication.SharedApplication.KeyWindow.RootViewController;

        public Task ShowDialogAsync(string title, string message)
        {
            return ShowDialogAsync(title, message, "OK", null);
        }

        public Task ShowDialogAsync(string title, string message, string buttonText, Action afterHideCallback)
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

        private static UIAlertActionStyle GetButtonStyle(DialogActionTypeEnum actionType)
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
                return deviceInfo.FamilyType == DeviceFamilyTypeEnum.Tablet;
            }
            catch
            {
                return false;
            }
        }
    }
}
