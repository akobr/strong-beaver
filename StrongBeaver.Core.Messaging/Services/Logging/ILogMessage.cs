using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace StrongBeaver.Core.Services.Logging
{
    public interface ILogMessage : IServiceMessage
    {
        LogLevel Level { get; }

        string Message { get; }

        IList<object> Arguments { get; }

        bool ContainsArguments { get; }

        void PerformMessage(ILogService service);
    }
}