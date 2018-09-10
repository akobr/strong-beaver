using System;
using System.Diagnostics;
using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Core.Container;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.Services
{
    public class ServiceProvider : ProviderWithMessageBus<IService>, IServiceProvider
    {
        public ServiceProvider(IContainer container, ILogService logService)
            : base(container)
        {
            Messenger = new Messenger();

            RegisterLogService(logService);
            LogDebug($"The Services provider '{GetType().Name}' has been created.");
        }

        public ServiceProvider(ILogService logService)
            : this(new SimpleIocContainer(), logService)
        {
            // no operataion
        }

        public IMessenger Messenger { get; }

        public ILogService Logger { get; private set; }

        public static IServiceProvider Current { get; private set; }

        protected void RegisterLogService(ILogService service)
        {
            if (service == null)
            {
                if (Logger != null)
                {
                    UnregisterFromMessageBus(Logger);
                }

                Logger = null;
                return;
            }

            RegisterToMessageBus<ILogService, ILogMessage>(service);
            Logger = service;

#if TRACE
            if (Logger == null)
            {
                return;
            }

            RegisterTraceLoggingForMessanger<IServiceMessage>(Messenger, service);
#endif
        }

        [Conditional("DEBUG")]
        protected void LogDebug(string message)
        {
            Logger?.Debug(message);
        }

        protected void LogError(string message, Exception exception)
        {
            Logger?.Error(message, exception);
        }

        [Conditional("TRACE")]
        private void RegisterTraceLoggingForMessanger<TMesage>(IMessenger messanger, ILogService logService)
            where TMesage : IMessage
        {
            Messenger.Register<TMesage>(logService, true, (message) =>
            {
                logService.Trace(
                    $"The message '{message.GetType().Name}' has been sended by service bus with code '{message.Code ?? "[NoN]"}'.",
                    message);
            });
        }

        public static void SetCurrentProvider(IServiceProvider newProvider)
        {
            Current = newProvider;
        }
    }
}