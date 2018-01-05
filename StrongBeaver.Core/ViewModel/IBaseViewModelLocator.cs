using GalaSoft.MvvmLight;
using StrongBeaver.Core.Platform;

namespace StrongBeaver.Core.ViewModel
{
    public interface IBaseViewModelLocator : IInitialisable, ICleanup
    {
        IPlatformInfo Platform { get; }

        bool IsAndroid { get; }

        bool IsAppleIos { get; }

        bool IsAppleMac { get; }

        bool IsWindowsUwp { get; }

        bool IsWindowsDesktop { get; }

        bool IsDebugMode { get; }
    }
}
