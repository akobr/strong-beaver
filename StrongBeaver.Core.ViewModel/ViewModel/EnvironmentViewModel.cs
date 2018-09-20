using StrongBeaver.Core.Platform;

namespace StrongBeaver.Core.ViewModel
{
    public class EnvironmentViewModel : BaseViewModel, IEnvironmentViewModel
    {
        private readonly IEnvironmentInfo environmentInfo;

        public EnvironmentViewModel(IPlatformProvider platformProvider)
        {
            environmentInfo = platformProvider.EnvironmentInfo;
        }

        public IEnvironmentInfo Info => environmentInfo;

        public bool IsAndroid => environmentInfo.Platform.PlatformType == PlatformTypeEnum.Android;

        public bool IsAppleIos => environmentInfo.Platform.PlatformType == PlatformTypeEnum.AppleIOS;

        public bool IsAppleMac => environmentInfo.Platform.PlatformType == PlatformTypeEnum.AppleMac;

        public bool IsWindowsUwp => environmentInfo.Platform.PlatformType == PlatformTypeEnum.WindowsUWP;

        public bool IsWindowsDesktop => environmentInfo.Platform.PlatformType == PlatformTypeEnum.WindowsDesktop;

        public bool IsWebSite => environmentInfo.Platform.PlatformType == PlatformTypeEnum.WebSite;

        public bool IsServer => environmentInfo.Platform.PlatformType == PlatformTypeEnum.Server;
    }
}