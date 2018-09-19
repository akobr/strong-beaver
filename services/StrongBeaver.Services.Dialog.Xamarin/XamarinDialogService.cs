using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Reflection;
using Xamarin.Forms;

namespace StrongBeaver.Services.Dialog
{
    public class XamarinDialogService : BaseDisposableService, IDialogService
    {
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

        public Task ShowDialogAsync(string dialogKey)
        {
            Type dialogType = GetDialogType(dialogKey);
            return ShowDialogAsync(dialogType);
        }

        public Task ShowDialogAsync(Type dialogType)
        {
            PopupPage dialog = (PopupPage)reflectionService.CreateInstance(dialogType);
            return xamarinNavigation.PushPopupAsync(dialog);
        }

        public Task ShowDialogAsync(string title, string message)
        {
            return platformSpecificDialogService.ShowDialogAsync(title, message);
        }

        public Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions)
        {
            return platformSpecificDialogService.ShowDialogAsync(title, message, actions);
        }

        public void ProcessMessage(IDialogMessage message)
        {
            message?.PerformMessage(this);
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