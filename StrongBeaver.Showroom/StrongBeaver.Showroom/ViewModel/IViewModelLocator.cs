using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public interface IViewModelLocator : IBaseViewModelLocator
    {
        IMainViewModel Main { get; }

        ExemplaryViewModel Exemplary { get; }
    }
}
