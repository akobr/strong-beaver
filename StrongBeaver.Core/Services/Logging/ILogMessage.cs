using System;
using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Logging
{
    public interface ILogMessage : IServiceMessage
    {
        LogMessageLevelEnum Level { get; }

        string Message { get; }

        IList<object> Arguments { get; }

        bool ContainsArguments { get; }

        void PerformMessage(ILogService service);
    }
}