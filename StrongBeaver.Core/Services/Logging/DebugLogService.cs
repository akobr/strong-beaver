using StrongBeaver.Core.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrongBeaver.Core.Services.Logging
{
    public class DebugLogService : BaseService, ILogService
    {
        public void Trace(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine($"[{nameof(LogMessageLevelEnum.Trace)}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void Debug(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine($"[{nameof(LogMessageLevelEnum.Debug)}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void Info(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine($"[{nameof(LogMessageLevelEnum.Info)}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void Warn(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine($"[{nameof(LogMessageLevelEnum.Warn)}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void Error(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine($"[{nameof(LogMessageLevelEnum.Error)}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void Fatal(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine($"[{nameof(LogMessageLevelEnum.Fatal)}]: {message ?? "NULL"}");
            WriteObjectsToDebug(args);
        }

        public void ProcessMessage(ILogMessage message)
        {
            if (message == null)
            {
                return;
            }

            message.PerformMessage(this);
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

            System.Diagnostics.Debug.WriteLine(objectToWrite);
            Exception exception = objectToWrite as Exception;

            if (exception == null)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine("EXCEPTION DETAILS");
            WriteExceptionToDebug(exception, 0);
        }

        private static void WriteExceptionToDebug(Exception exception, int nestingLevel)
        {
            if (exception == null
                || nestingLevel > GlobalConstatns.EXCEPTION_MAX_NESTING_LEVEL)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine($"MESSAGE: {exception.Message ?? string.Empty}");
            System.Diagnostics.Debug.WriteLine($"SOURCE: {exception.Source ?? string.Empty}");
            System.Diagnostics.Debug.WriteLine(exception.StackTrace ?? string.Empty);
            WriteExceptionDataToDebug(exception);

            if (exception.InnerException == null)
            {
                return;
            }

            int nextNestingLevel = nestingLevel + 1;
            System.Diagnostics.Debug.WriteLine($"INNER EXCEPTION LEVEL {nextNestingLevel}");
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
            System.Diagnostics.Debug.WriteLine("EXCEPTION DATA:");
            System.Diagnostics.Debug.WriteLine(stb.ToString());
        }
    }
}