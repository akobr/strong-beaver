using System;

namespace BeaverSoft.Core.Services.Logging
{
    public class LogTraceMessage : LogMessage
    {
        public LogTraceMessage(string message)
            : base(LogMessageLevelEnum.Trace, message)
        {
            // Empty
        }

        public LogTraceMessage(string message, params object[] args)
            : base(LogMessageLevelEnum.Trace, message, args)
        {
            // Empty
        }

        public LogTraceMessage(string message, Exception exception)
            : base(LogMessageLevelEnum.Trace, message, exception)
        {
            // Empty
        }

        public override void PerformSimpleMessage(ILogService service)
        {
#if TRACE
            service.Trace(Message);
#endif
        }

        public override void PerformExceptionMessage(ILogService service)
        {
#if TRACE
            service.Trace(Message, Exception);
#endif
        }

        public override void PerformArgumentsMessage(ILogService service)
        {
#if TRACE
            service.Trace(Message, Arguments);
#endif
        }
    }
}