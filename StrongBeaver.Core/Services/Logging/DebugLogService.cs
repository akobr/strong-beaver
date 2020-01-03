using Microsoft.Extensions.Logging;
using StrongBeaver.Core.Constants;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace StrongBeaver.Core.Services.Logging
{
    public class DebugLogService : ILogService, ILogger, ILoggerProvider, ILoggerFactory
    {
        private readonly ConcurrentBag<ILoggerProvider> providers;

        public DebugLogService()
        {
            providers = new ConcurrentBag<ILoggerProvider>();
        }

        public void Log(LogLevel level, string message, params object[] args)
        {
            Debug.WriteLine($"[{level}]: {message ?? GlobalConstatns.DEFAULT_NULL_STRING}");
            WriteObjectsToDebug(args);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Debug.WriteLine($"[{logLevel}, {eventId.Id}]: {BuildMessage(eventId, state, exception, formatter) ?? GlobalConstatns.DEFAULT_NULL_STRING}");
            WriteObjectsToDebug(new object[] { eventId, state, exception });
        }

        public void AddProvider(ILoggerProvider provider)
        {
            providers.Add(provider);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
#if DEBUG
                    return true;
#else
                    return false;
#endif

                case LogLevel.Trace:
#if TRACE
                    return true;
#else
                    return false;
#endif

                case LogLevel.None:
                    return false;

                default:
                    return true;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new DebugScope<TState>(this, state);
        }

        public ILogger CreateLogger(string categoryName)
        {
            foreach (ILoggerProvider subProvider in providers)
            {
                ILogger logger = subProvider.CreateLogger(categoryName);

                if (logger != null)
                {
                    return logger;
                }
            }

            return this;
        }

        public void Dispose()
        {
            while (providers.TryTake(out ILoggerProvider subProvider))
            {
                subProvider?.Dispose();
            }
        }

        private static string BuildMessage<TState>(EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                return formatter(state, exception);
            }

            return exception?.Message ?? eventId.Name ?? GlobalConstatns.DEFAULT_NULL_STRING;
        }

        [Conditional("DEBUG")]
        private static void WriteObjectsToDebug(IEnumerable<object> args)
        {
            if (args == null)
            {
                return;
            }

            foreach (object objectToWrite in args)
            {
                WriteObjectToDebug(objectToWrite);
            }
        }

        private static void WriteObjectToDebug(object objectToWrite)
        {
            if (objectToWrite == null)
            {
                return;
            }

            Debug.WriteLine(objectToWrite);

            switch (objectToWrite)
            {
                case IFormattable formattable:
                    string formattedText = formattable.ToString(null, CultureInfo.InvariantCulture);
                    if (string.IsNullOrEmpty(formattedText))
                    {
                        Debug.WriteLine(formattedText);
                    }
                    break;

                case Exception exception:
                    Debug.WriteLine("EXCEPTION DETAILS");
                    WriteExceptionToDebug(exception, 0);
                    break;
            }
        }

        private static void WriteExceptionToDebug(Exception exception, int nestingLevel)
        {
            if (exception == null
                || nestingLevel > GlobalConstatns.EXCEPTION_MAX_NESTING_LEVEL)
            {
                return;
            }

            Debug.WriteLine($"MESSAGE: {exception.Message ?? string.Empty}");
            Debug.WriteLine($"SOURCE: {exception.Source ?? string.Empty}");
            Debug.WriteLine(exception.StackTrace ?? string.Empty);
            WriteExceptionDataToDebug(exception);

            if (exception.InnerException == null)
            {
                return;
            }

            int nextNestingLevel = nestingLevel + 1;
            Debug.WriteLine($"INNER EXCEPTION LEVEL {nextNestingLevel}");
            WriteExceptionToDebug(exception.InnerException, nextNestingLevel);
        }

        private static void WriteExceptionDataToDebug(Exception exception)
        {
            if (exception?.Data == null || exception.Data.Count < 1)
            {
                return;
            }

            StringBuilder stb = new StringBuilder();

            foreach (DictionaryEntry entry in exception.Data)
            {
                stb.Append(entry.Key ?? "[Unknown]");
                stb.Append(" = ");
                stb.Append(entry.Value ?? string.Empty);
                stb.Append("; ");
            }

            if (stb.Length < 2)
            {
                return;
            }

            stb.Remove(stb.Length - 2, 2);
            Debug.WriteLine("EXCEPTION DATA:");
            Debug.WriteLine(stb.ToString());
        }

        private class DebugScope<TState> : IDisposable
        {
            private readonly ILogService logger;
            private readonly TState state;

            public DebugScope(ILogService logger, TState state)
            {
                this.logger = logger;
                this.state = state;

                logger.Debug("START OF SCOPE", state);
            }

            public void Dispose()
            {
                logger.Debug("END OF SCOPE", state);
            }
        }
    }
}