using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public interface IViewModelLocator : IBaseViewModelLocator
    {
        IMainViewModel MainViewModel { get; }

        ExemplaryViewModel Exemplary { get; }
    }
}
