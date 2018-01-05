using System;

namespace BeaverSoft.Core.Services.Logging
{
    public class LogFatalMessage : LogMessage
    {
        public LogFatalMessage(string message)
            : base(LogMessageLevelEnum.Fatal, message)
        {
            // Empty
        }

        public LogFatalMessage(string message, params object[] args)
            : base(LogMessageLevelEnum.Fatal, message, args)
        {
            // Empty
        }

        public LogFatalMessage(string message, Exception exception)
            : base(LogMessageLevelEnum.Fatal, message, exception)
        {
            // Empty
        }

        public override void PerformSimpleMessage(ILogService service)
        {
            service.Fatal(Message);
        }

        public override void PerformExceptionMessage(ILogService service)
        {
            service.Fatal(Message, Exception);
        }

        public override void PerformArgumentsMessage(ILogService service)
        {
            service.Fatal(Message, Arguments);
        }
    }
}