using Microsoft.Extensions.Logging;

namespace StrongBeaver.Core.Services.Logging
{
    public interface ILogService : IService
    {
        void Log(LogLevel level, string message, params object[] args);
    }
}