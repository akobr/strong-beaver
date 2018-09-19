using System;

namespace StrongBeaver.Services.Toast
{
    public interface IToast
    {
        string Message { get; }

        TimeSpan Duration { get; }

        string ActionTitle { get; }

        Action Action { get; }
    }
}