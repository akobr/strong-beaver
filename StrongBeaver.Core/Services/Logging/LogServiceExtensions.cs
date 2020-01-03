using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace StrongBeaver.Core.Services.Logging
{
    public static class LogServiceExtensions
    {
        [Conditional("TRACE")]
        public static void Trace(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogLevel.Trace, message, args);
        }

        [Conditional("DEBUG")]
        public static void Debug(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogLevel.Debug, message, args);
        }

        public static void Info(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogLevel.Information, message, args);
        }

        public static void Warn(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogLevel.Warning, message, args);
        }

        public static void Error(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogLevel.Error, message, args);
        }

        public static void Fatal(this ILogService service, string message, params object[] args)
        {
            Critical(service, message, args);
        }

        public static void Critical(this ILogService service, string message, params object[] args)
        {
            service?.Log(LogLevel.Critical, message, args);
        }
    }
}