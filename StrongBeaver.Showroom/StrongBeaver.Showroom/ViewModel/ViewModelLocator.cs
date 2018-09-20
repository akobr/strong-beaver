using CommonServiceLocator;
using StrongBeaver.Core.Platform;
using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public class ViewModelLocator : BaseViewModelLocator, IViewModelLocator
    {
        public ViewModelLocator()
            : base(ServiceLocator.Current.GetInstance<IPlatformInfo>())
        {
            MainViewModel = ServiceLocator.Current.GetInstance<IMainViewModel>();
            RegisterViewModel(MainViewModel);

            Exemplary = ServiceLocator.Current.GetInstance<ExemplaryViewModel>();
            RegisterViewModel(Exemplary);
        }

        public static IViewModelLocator Current { get; private set; }

        public IMainViewModel MainViewModel { get; }

        public ExemplaryViewModel Exemplary { get; }

        public static void SetCurrentLocator(IViewModelLocator viewModelLocator)
        {
            Current = viewModelLocator;
        }
    }
}
