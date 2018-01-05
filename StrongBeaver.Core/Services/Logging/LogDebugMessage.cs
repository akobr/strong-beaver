using System;

namespace BeaverSoft.Core.Services.Logging
{
    public class LogDebugMessage : LogMessage
    {
        public LogDebugMessage(string message)
            : base(LogMessageLevelEnum.Debug, message)
        {
            // Empty
        }

        public LogDebugMessage(string message, params object[] args)
            : base(LogMessageLevelEnum.Debug, message, args)
        {
            // Empty
        }

        public LogDebugMessage(string message, Exception exception)
            : base(LogMessageLevelEnum.Debug, message, exception)
        {
            // Empty
        }

        public override void PerformSimpleMessage(ILogService service)
        {
#if DEBUG
            service.Debug(Message); 
#endif
        }

        public override void PerformExceptionMessage(ILogService service)
        {
#if DEBUG
            service.Debug(Message, Exception); 
#endif
        }

        public override void PerformArgumentsMessage(ILogService service)
        {
#if DEBUG
            service.Debug(Message, Arguments); 
#endif
        }
    }
}