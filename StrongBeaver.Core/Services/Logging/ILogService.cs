namespace StrongBeaver.Core.Services.Logging
{
    public interface ILogService : IService, IMessageBusService<ILogMessage>
    {
        // These is TRACE method and should be labeled with CONDITIONAL TRACE attribute.
        void Trace(string message, params object[] args);

        // These is DEBUG method and should be labeled with CONDITIONAL DEBUG attribute.
        void Debug(string message, params object[] args);

        void Info(string message, params object[] args);

        void Warn(string message, params object[] args);

        void Error(string message, params object[] args);

        void Fatal(string message, params object[] args);
    }
}