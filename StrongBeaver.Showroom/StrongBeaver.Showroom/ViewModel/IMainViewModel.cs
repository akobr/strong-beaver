using StrongBeaver.Core.ViewModel;
using System.Windows.Input;

namespace StrongBeaver.Showroom.ViewModel
{
    public interface IMainViewModel : IViewModel
    {
        ICommand NavigateToArchitecturePageCommand { get; }

        ICommand NavigateToDialogPageCommand { get; }

        ICommand NavigateToDeviceInfoPageCommand { get; }

        ICommand NavigateToStoragePageCommand { get; }

        ICommand NavigateToNetworkPageCommand { get; }

        ICommand NavigateToWebContentPageCommand { get; }
    }
}