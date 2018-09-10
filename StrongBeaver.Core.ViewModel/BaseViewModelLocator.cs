using StrongBeaver.Core.Platform;
using System.Collections.Generic;
using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public abstract class BaseViewModelLocator : IBaseViewModelLocator
    {
        private readonly IList<IViewModel> models;

        protected BaseViewModelLocator(IPlatformInfo platformInfo)
        {
            Platform = platformInfo;
            Messanger = new Messenger();
            models = new List<IViewModel>();
        }

        public IPlatformInfo Platform { get; }

        public IMessenger Messanger { get; }

        public bool IsAndroid => Platform.PlatformType == PlatformTypeEnum.Android;

        public bool IsAppleIos => Platform.PlatformType == PlatformTypeEnum.AppleIOS;

        public bool IsAppleMac => Platform.PlatformType == PlatformTypeEnum.AppleMac;

        public bool IsWindowsUwp => Platform.PlatformType == PlatformTypeEnum.WindowsUWP;

        public bool IsWindowsDesktop => Platform.PlatformType == PlatformTypeEnum.WindowsDesktop;

        public bool IsWeb => Platform.PlatformType == PlatformTypeEnum.Web;

        public bool IsWebSinglePage => Platform.PlatformType == PlatformTypeEnum.WebSinglePage;

        public bool IsServer => Platform.PlatformType == PlatformTypeEnum.Server;

        public bool IsDebugMode => IsDebugModeStatic;

        public static bool IsDebugModeStatic
#if DEBUG
            => true;
#else
            => false;
#endif

        public void Initialise()
        {
            OnInitialise();
        }

        public void Cleanup()
        {
            OnCleanup();
        }

        protected virtual void OnInitialise()
        {
            foreach (IViewModel model in models)
            {
                model.Initialise();
            }
        }

        protected virtual void OnCleanup()
        {
            foreach (IViewModel model in models)
            {
                model.Cleanup();
            }
        }

        protected void RegisterViewModel(IViewModel model)
        {
            if (model == null)
            {
                return;
            }

            models.Add(model);
        }

        protected void RegisterViewModelWithMessageBus<TRecipient, TMessage>(TRecipient model)
            where TRecipient : class, IMessageBusViewModel<TMessage>
            where TMessage : IViewModelMessage
        {
            if (model == null)
            {
                return;
            }

            models.Add(model);
            Messanger.Register<TRecipient, TMessage>(model);
        }
    }
}
