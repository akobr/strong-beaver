using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Logging
{
    public class LogMessage : ServiceMessage, ILogMessage
    {
        public LogMessage()
        {
            // No operation
        }

        public LogMessage(string message)
            : this(LogMessageLevelEnum.Error, message)
        {
            // No operation
        }

        public LogMessage(LogMessageLevelEnum level, string message)
        {
            Level = level;
            Message = message;
            Arguments = new List<object>();
        }

        public LogMessage(LogMessageLevelEnum level, string message, params object[] args)
        {
            Level = level;
            Message = message;
            Arguments = new List<object>(args);
        }

        public LogMessage(object sender)
            : base(sender)
        {
            Level = LogMessageLevelEnum.Error;
            Message = null;
            Arguments = new List<object>();
        }

        public LogMessage(object sender, string message)
            : base(sender)
        {
            Level = LogMessageLevelEnum.Error;
            Message = message;
            Arguments = new List<object>();
        }

        public LogMessageLevelEnum Level { get; }

        public string Message { get; }

        public IList<object> Arguments { get; }

        public bool ContainsArguments
        {
            get { return Arguments.Count > 0; }

        }

        public void PerformMessage(ILogService service)
        {
            switch (Level)
            {
                case LogMessageLevelEnum.Trace:
                    service.Trace(Message, Arguments);
                    break;

                case LogMessageLevelEnum.Debug:
                    service.Debug(Message, Arguments);
                    break;

                case LogMessageLevelEnum.Info:
                    service.Info(Message, Arguments);
                    break;

                case LogMessageLevelEnum.Warn:
                    service.Warn(Message, Arguments);
                    break;

                case LogMessageLevelEnum.Fatal:
                    service.Fatal(Message, Arguments);
                    break;

                default:
                    service.Error(Message, Arguments);
                    break;
            }
        }
    }
}
