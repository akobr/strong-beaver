using System;
using Microsoft.Extensions.Logging;

namespace StrongBeaver.Core.Services.Logging
{
    public class EmptyLogService : ILogService, ILogger, ILoggerProvider, ILoggerFactory
    {
        public void Log(LogLevel level, string message, params object[] args)
        {
            // no operation
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // no operation
        }

        public ILogger CreateLogger(string categoryName)
        {
            return this;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new EmptyDispose();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void AddProvider(ILoggerProvider provider)
        {
            // no operation
        }

        public void Dispose()
        {
            // no operation
        }

        private class EmptyDispose : IDisposable
        {
            public void Dispose()
            {
                // no operation
            }
        }
    }
}
