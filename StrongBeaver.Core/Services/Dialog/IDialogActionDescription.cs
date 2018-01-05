using System;

namespace StrongBeaver.Core.Services.Dialog
{
    public interface IDialogActionDescription
    {
        Action Action { get; }

        string Title { get; }

        DialogActionTypeEnum Type { get; }
    }
}