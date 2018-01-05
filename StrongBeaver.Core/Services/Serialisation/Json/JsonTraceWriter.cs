using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace StrongBeaver.Core.Services.Serialization.Json
{
    public class JsonTraceWriter : ITraceWriter
    {
        public void Trace(TraceLevel level, string message, Exception ex)
        {
            switch (level)
            {

                case TraceLevel.Error:
                    Provider.LogErrorMessage(message, ex);
                    break;

                case TraceLevel.Warning:
                    Provider.LogWarningMessage(message, ex);
                    break;

                case TraceLevel.Verbose:
                    Provider.LogTraceMessage(message, ex);
                    break;

                case TraceLevel.Info:
                    Provider.LogDebugMessage("[Info] " + message, ex);
                    break;
            }
        }

        public TraceLevel LevelFilter { get; internal set; }
    }
}