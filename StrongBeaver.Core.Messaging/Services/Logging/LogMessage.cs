using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace StrongBeaver.Core.Services.Logging
{
    public class LogMessage : ServiceMessage, ILogMessage
    {
        public LogMessage()
        {
            // No operation
        }

        public LogMessage(string errorMessage)
            : this(LogLevel.Error, errorMessage)
        {
            // No operation
        }

        public LogMessage(LogLevel level, string message)
        {
            Level = level;
            Message = message;
            Arguments = new List<object>();
        }

        public LogMessage(LogLevel level, string message, params object[] args)
        {
            Level = level;
            Message = message;
            Arguments = new List<object>(args);
        }

        public LogLevel Level { get; }

        public string Message { get; }

        public IList<object> Arguments { get; }

        public bool ContainsArguments => Arguments.Count > 0;

        public void PerformMessage(ILogService service)
        {
            service.Log(Level, Message, Arguments);
        }
    }
}
