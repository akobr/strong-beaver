using System.Diagnostics;

namespace StrongBeaver.Core.Services.Logging
{
    public static class LogServiceExtensions
    {
        [Conditional("TRACE")]
        public static void Trace(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogMessageLevelEnum.Trace, message, args);
        }

        [Conditional("DEBUG")]
        public static void Debug(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogMessageLevelEnum.Debug, message, args);
        }

        public static void Info(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogMessageLevelEnum.Info, message, args);
        }

        public static void Warn(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogMessageLevelEnum.Warn, message, args);
        }

        public static void Error(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogMessageLevelEnum.Error, message, args);
        }
    }
}