using System;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Core.Services.Serialisation.Json
{
    public class JsonTraceWriter : ITraceWriter
    {
        private readonly ILogService logging;

        public JsonTraceWriter(ILogService logging)
        {
            this.logging = logging;
        }

        public void Trace(TraceLevel level, string message, Exception ex)
        {
            switch (level)
            {

                case TraceLevel.Error:
                    logging.Error(message, ex);
                    break;

                case TraceLevel.Warning:
                    logging.Warn(message, ex);
                    break;

                case TraceLevel.Verbose:
                    logging.Trace(message, ex);
                    break;

                case TraceLevel.Info:
                    logging.Info(message, ex);
                    break;
            }
        }

        public TraceLevel LevelFilter { get; internal set; }
    }
}