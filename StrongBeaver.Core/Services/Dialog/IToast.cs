using System;

namespace StrongBeaver.Core.Services.Dialog
{
    public interface IToast
    {
        string Message { get; }

        TimeSpan Duration { get; }

        string ActionTitle { get; }

        Action Action { get; }
    }
}