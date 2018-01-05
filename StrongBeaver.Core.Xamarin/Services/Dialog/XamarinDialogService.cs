using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongBeaver.Core.Services.Reflection;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace StrongBeaver.Core.Services.Dialog
{
    public class XamarinDialogService : BaseService, IDialogService
    {
        private const string GENERIC_ERROR_MESSAGE = "A generic error occurred.";

        private readonly IInstanceCreationService reflectionService;
        private readonly IPlatformSpecificDialogService platformSpecificDialogService;
        private readonly IDictionary<string, Type> registeredDialogs;

        private INavigation xamarinNavigation;

        public XamarinDialogService(
            IInstanceCreationService reflectionService,
            IPlatformSpecificDialogService platformSpecificDialogService)
        {
            this.reflectionService = reflectionService;
            this.platformSpecificDialogService = platformSpecificDialogService;
            registeredDialogs = new Dictionary<string, Type>();
        }

        public void Initialise(INavigation navigation, bool supportForwarding = false)
        {
            xamarinNavigation = navigation;
        }

        public void Configure(string dialogKey, Type dialogType)
        {
            if (dialogType == null)
            {
                registeredDialogs.Remove(dialogKey);
                return;
            }

            registeredDialogs[dialogKey] = dialogType;
        }

        public Task ShowDialogByNameAsync(string dialogName)
        {
            Type dialogType = GetDialogType(dialogName);
            PopupPage dialog = (PopupPage)reflectionService.CreateInstance(dialogType);
            return ShowDialogAsync(dialog);
        }

        public Task ShowDialogAsync(object content)
        {
            PopupPage dialog = (PopupPage)content;
            return xamarinNavigation.PushPopupAsync(dialog);
        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return ShowMessage(
                title,
                string.IsNullOrWhiteSpace(message) ? GENERIC_ERROR_MESSAGE : message,
                buttonText,
                afterHideCallback);
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            return ShowMessage(
                title,
                error?.Message ?? GENERIC_ERROR_MESSAGE,
                buttonText,
                afterHideCallback);
        }

        public Task ShowMessage(string message, string title)
        {
            return platformSpecificDialogService.ShowMessage(message, title);
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            return ShowDialogAsync(
                title,
                message,
                new[] { new DialogActionDescription(buttonText, afterHideCallback) });
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            int buttonNumber = await ShowDialogAsync(
                title,
                message,
                new[]
                {
                    new DialogActionDescription(buttonCancelText, DialogActionTypeEnum.Negative, () => { afterHideCallback?.Invoke(false); }),
                    new DialogActionDescription(buttonConfirmText, DialogActionTypeEnum.Positive, () => { afterHideCallback?.Invoke(true); })
                });

            return buttonNumber > 0;
        }

        public Task ShowMessageBox(string message, string title)
        {
            return ShowMessage(message, title);
        }

        public Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions)
        {
            return platformSpecificDialogService.ShowDialogAsync(title, message, actions);
        }

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
            if (toast == null)
            {
                return;
            }

            platformSpecificDialogService.ShowToast(toast);
        }

        public void ProcessMessage(IDialogMessage message)
        {
            if (message == null)
            {
                return;
            }

            message.PerformMessage(this);
        }

        private Type GetDialogType(string dialogName)
        {
            if (!registeredDialogs.ContainsKey(dialogName))
            {
                throw new ArgumentException(
                    $"No such dialog: {dialogName}. Did you forget to call DialogService.Configure?",
                    nameof(dialogName));
            }

            return registeredDialogs[dialogName];
        }
    }
}