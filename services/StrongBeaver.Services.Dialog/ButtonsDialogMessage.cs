using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Dialog
{
    public class ButtonsDialogMessage : ServiceAsyncMesssage<int>, IDialogMessage
    {
        private readonly List<IDialogActionDescription> actions = new List<IDialogActionDescription>();

        public ButtonsDialogMessage()
        {
            // No operation
        }

        public ButtonsDialogMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public IReadOnlyList<IDialogActionDescription> Actions => actions;

        public void AddAction(IDialogActionDescription action)
        {
            actions.Add(action);
        }

        public void AddAction(string title, Action action)
        {
            AddAction(new DialogActionDescription(title, action));
        }

        public void AddAction(string title, DialogActionTypeEnum type, Action action)
        {
            AddAction(new DialogActionDescription(title, type, action));
        }

        public void PerformMessage(IDialogService service)
        {
            OperationHolder = new TaskHolder<int>(service.ShowDialogAsync(Title, Message, actions));
        }
    }
}