namespace StrongBeaver.Core.Services.Logging
{
    public class EmptyLogService : ILogService
    {
        public void Log(LogMessageLevelEnum level, string message, params object[] args)
        {
            // no operation
        }

        public void ProcessMessage(ILogMessage message)
        {
            // no operation
        }
    }
}
