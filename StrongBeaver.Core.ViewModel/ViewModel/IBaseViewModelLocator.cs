using GalaSoft.MvvmLight;

namespace StrongBeaver.Core.ViewModel
{
    public interface IBaseViewModelLocator : IInitialisable, ICleanup
    {
        IEnvironmentViewModel Environment { get; }

        bool IsDebugMode { get; }
    }
}
