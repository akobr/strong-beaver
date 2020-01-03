namespace StrongBeaver.Core.Services.Logging
{
    public class LogServiceMessageBusAdapter : IMessageBusService<ILogMessage>
    {
        private readonly ILogService logService;

        public LogServiceMessageBusAdapter(ILogService logService)
        {
            this.logService = logService;
        }

        public void ProcessMessage(ILogMessage message)
        {
            message?.PerformMessage(logService);
        }
    }
}
