using System;

namespace BeaverSoft.Core.Services.Logging
{
    public class LogWarnMessage : LogMessage
    {
        public LogWarnMessage(string message)
            : base(LogMessageLevelEnum.Warn, message)
        {
            // Empty
        }

        public LogWarnMessage(string message, params object[] args)
            : base(LogMessageLevelEnum.Warn, message, args)
        {
            // Empty
        }

        public LogWarnMessage(string message, Exception exception)
            : base(LogMessageLevelEnum.Warn, message, exception)
        {
            // Empty
        }

        public override void PerformSimpleMessage(ILogService service)
        {
            service.Warn(Message);
        }

        public override void PerformExceptionMessage(ILogService service)
        {
            service.Warn(Message, Exception);
        }

        public override void PerformArgumentsMessage(ILogService service)
        {
            service.Warn(Message, Arguments);
        }
    }
}