using System;

namespace StrongBeaver.Core.Services.Dialog
{
    public class DialogActionDescription : IDialogActionDescription
    {
        public DialogActionDescription()
        {
            // No operation
        }

        public DialogActionDescription(string title, Action action)
            : this(title, DialogActionTypeEnum.Neutral, action)
        {
            // No operation
        }

        public DialogActionDescription(string title, DialogActionTypeEnum type, Action action)
        {
            Title = title;
            Type = type;
            Action = action;
        }

        public string Title { get; set; }

        public DialogActionTypeEnum Type { get; set; }

        public Action Action { get; set; }
    }
}
