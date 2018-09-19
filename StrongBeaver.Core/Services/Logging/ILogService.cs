namespace StrongBeaver.Core.Services.Logging
{
    public interface ILogService : IService, IMessageBusService<ILogMessage>
    {
        void Log(LogMessageLevelEnum level, string message, params object[] args);

        
    }
}