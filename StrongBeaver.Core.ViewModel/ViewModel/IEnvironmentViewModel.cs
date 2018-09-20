using StrongBeaver.Core.Platform;

namespace StrongBeaver.Core.ViewModel
{
    public interface IEnvironmentViewModel : IViewModel
    {
        IEnvironmentInfo Info { get; }

        bool IsAndroid { get; }

        bool IsAppleIos { get; }

        bool IsAppleMac { get; }

        bool IsWindowsUwp { get; }

        bool IsWindowsDesktop { get; }

        bool IsWebSite { get; }

        bool IsServer { get; }
    }
}