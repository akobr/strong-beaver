namespace StrongBeaver.Core.Services.Logging
{
    public class EmptyLogService : BaseService, ILogService
    {
        public void Debug(string message, params object[] args)
        {
            // No operation
        }

        public void Error(string message, params object[] args)
        {
            // No operation
        }

        public void Fatal(string message, params object[] args)
        {
            // No operation
        }

        public void Info(string message, params object[] args)
        {
            // No operation
        }

        public void ProcessMessage(ILogMessage message)
        {
            // No operation
        }

        public void Trace(string message, params object[] args)
        {
            // No operation
        }

        public void Warn(string message, params object[] args)
        {
            // No operation
        }
    }
}
