using StrongBeaver.Core.Platform;
using System.Collections.Generic;

namespace StrongBeaver.Core.ViewModel
{
    public abstract class BaseViewModelLocator : IBaseViewModelLocator
    {
        private readonly IPlatformInfo platformInfo;
        private readonly IList<IViewModel> models;

        protected BaseViewModelLocator(IPlatformInfo platformInfo)
        {
            this.platformInfo = platformInfo;
            models = new List<IViewModel>();
        }

        public IPlatformInfo Platform => platformInfo;

        public bool IsAndroid => platformInfo.PlatformType == PlatformTypeEnum.Android;

        public bool IsAppleIos => platformInfo.PlatformType == PlatformTypeEnum.AppleIOS;

        public bool IsAppleMac => platformInfo.PlatformType == PlatformTypeEnum.AppleMac;

        public bool IsWindowsUwp => platformInfo.PlatformType == PlatformTypeEnum.WindowsUWP;

        public bool IsWindowsDesktop => platformInfo.PlatformType == PlatformTypeEnum.WindowsDesktop;

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

            Provider.LogDebugMessage($"The ViewModel provider '{GetType().Name}' has been initialised.");
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
            models.Add(model);
        }
    }
}
