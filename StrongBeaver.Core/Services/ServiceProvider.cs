using System;
using System.Diagnostics;
using StrongBeaver.Core.Services.Logging;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Ioc;
using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Core.Services
{
    public class ServiceProvider : ProviderWithMessageBus<IService>, IServiceProvider
    {
        private readonly IMessenger messenger;

        public ServiceProvider(ISimpleIoc container, ILogService logService)
            : base(container)
        {
            messenger = new Messenger();

            RegisterLogService(logService);
            LogDebug($"The Services provider '{GetType().Name}' has been created.");
        }

        public IMessenger Messenger => messenger;

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

            RegisterTraceLoggingForMessanger<IServiceMessage>(messenger, service);
            RegisterTraceLoggingForMessanger<IViewModelMessage>(messenger, service);
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
            messenger.Register<TMesage>(logService, true, (message) =>
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