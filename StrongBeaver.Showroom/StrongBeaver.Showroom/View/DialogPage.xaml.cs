﻿using System;
using System.Collections.Generic;
using StrongBeaver.Services.Dialog;
using StrongBeaver.Services.Toast;
using StrongBeaver.Showroom.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StrongBeaver.Showroom.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogPage : ContentPage
    {
        private readonly IDialogService dialogService;
        private readonly IToastService toastService;

        public DialogPage(IDialogService dialogService, IToastService toastService)
        {
            this.dialogService = dialogService;
            this.toastService = toastService;

            InitializeComponent();
        }

        private async void HandleShowDialogClicked(object sender, EventArgs e)
        {
            string title = string.IsNullOrWhiteSpace(entryDialogTitle.Text) ? "Nice looking title" : entryDialogTitle.Text;
            string text = string.IsNullOrWhiteSpace(entryDialogText.Text) ? "Sample message body." : entryDialogText.Text;

            int tappedButton = await dialogService.ShowDialogAsync(title, text, GetDialogActions());
            toastService.ShowToast(new Toast($"The button '{GetButtonTitle(tappedButton)}' has been tapped."));
        }

        private void HandleShowToastClicked(object sender, EventArgs e)
        {
            string text = string.IsNullOrWhiteSpace(entryToastText.Text) ? "Simple toast message." : entryToastText.Text;
            Toast toast = new Toast(text)
            {
                Duration = switchToastDismiss.On ? TimeSpan.FromSeconds(3.0) : TimeSpan.FromSeconds(20.0),
                ActionTitle = "OK",
                Action = () => { dialogService.ShowDialogAsync("A toast message has been tapped.", "Action from toast"); }
            };

            toastService.ShowToast(toast);
        }

        private async void HandleShowCustomDialogClicked(object sender, EventArgs e)
        {
            await dialogService.ShowDialogAsync(ShowroomDialogKeys.EXEMPLARY_DIALOG);
        }

        private IReadOnlyCollection<IDialogActionDescription> GetDialogActions()
        {
            List<IDialogActionDescription> actions = new List<IDialogActionDescription>();

            actions.Add(new DialogActionDescription("Cancel", DialogActionTypeEnum.Negative, null));
            actions.Add(new DialogActionDescription("OK", DialogActionTypeEnum.Positive, null));

            if (switchDialogFiveButtons.On)
            {
                actions.Add(new DialogActionDescription("Option 1", null));
                actions.Add(new DialogActionDescription("Option 2", null));
                actions.Add(new DialogActionDescription("Option 3", null));
            }

            return actions;
        }

        private string GetButtonTitle(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 1:
                    return "Ok";

                case 2:
                    return "Option 1";

                case 3:
                    return "Option 2";

                case 4:
                    return "Option 3";

                default:
                    return "Cancel";
            }
        }
    }
}