using GalaSoft.MvvmLight;
using StrongBeaver.Core.Messaging;
using StrongBeaver.Core.Platform;

namespace StrongBeaver.Core.ViewModel
{
    public interface IBaseViewModelLocator : IInitialisable, ICleanup
    {
        IPlatformInfo Platform { get; }

        IMessenger Messanger { get; }

        bool IsAndroid { get; }

        bool IsAppleIos { get; }

        bool IsAppleMac { get; }

        bool IsWindowsUwp { get; }

        bool IsWindowsDesktop { get; }

        bool IsWeb { get; }

        bool IsWebSinglePage { get; }

        bool IsServer { get; }

        bool IsDebugMode { get; }
    }
}
