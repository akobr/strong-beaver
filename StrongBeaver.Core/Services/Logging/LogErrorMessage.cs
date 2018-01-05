using System;

namespace BeaverSoft.Core.Services.Logging
{
    public class LogErrorMessage : LogMessage
    {
        public LogErrorMessage(string message)
            : base(LogMessageLevelEnum.Error, message)
        {
            // Empty
        }

        public LogErrorMessage(string message, params object[] args)
            : base(LogMessageLevelEnum.Error, message, args)
        {
            // Empty
        }

        public LogErrorMessage(string message, Exception exception)
            : base(LogMessageLevelEnum.Error, message, exception)
        {
            // Empty
        }

        public override void PerformSimpleMessage(ILogService service)
        {
            service.Error(Message);
        }

        public override void PerformExceptionMessage(ILogService service)
        {
            service.Error(Message, Exception);
        }

        public override void PerformArgumentsMessage(ILogService service)
        {
            service.Error(Message, Arguments);
        }
    }
}