using System;

namespace StrongBeaver.Services.Toast
{
    public class Toast : IToast
    {
        public Toast(string message)
        {
            Message = message;
            Duration = TimeSpan.FromSeconds(2.5);
            ActionTitle = null;
            Action = null;
        }

        public string Message { get; set; }

        public TimeSpan Duration { get; set; }

        public string ActionTitle { get; set; }

        public Action Action { get; set; }
    }
}