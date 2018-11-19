using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public interface IViewModelProvider
    {
        IMainViewModel Main { get; }

        ExemplaryViewModel Exemplary { get; }

        IEnvironmentViewModel Environment { get; }
    }
}
