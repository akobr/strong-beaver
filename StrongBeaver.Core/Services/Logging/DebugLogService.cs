using StrongBeaver.Core.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrongBeaver.Core.Services.Logging
{
    public class DebugLogService : ILogService
    {
        public void Log(LogMessageLevelEnum level, string message, params object[] args)
        {
            Debug.WriteLine($"[{level}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void ProcessMessage(ILogMessage message)
        {
            message?.PerformMessage(this);
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
            Exception exception = objectToWrite as Exception;

            if (exception == null)
            {
                return;
            }

            Debug.WriteLine("EXCEPTION DETAILS");
            WriteExceptionToDebug(exception, 0);
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
    }
}