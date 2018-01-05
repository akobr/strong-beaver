using System;

namespace BeaverSoft.Core.Services.Logging
{
    public class LogInfoMessage : LogMessage
    {
        public LogInfoMessage(string message)
            : base(LogMessageLevelEnum.Info, message)
        {
            // Empty
        }

        public LogInfoMessage(string message, params object[] args)
            : base(LogMessageLevelEnum.Info, message, args)
        {
            // Empty
        }

        public LogInfoMessage(string message, Exception exception)
            : base(LogMessageLevelEnum.Info, message, exception)
        {
            // Empty
        }

        public override void PerformSimpleMessage(ILogService service)
        {
            service.Info(Message);
        }

        public override void PerformExceptionMessage(ILogService service)
        {
            service.Info(Message, Exception);
        }

        public override void PerformArgumentsMessage(ILogService service)
        {
            service.Info(Message, Arguments);
        }
    }
}