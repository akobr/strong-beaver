using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public class ViewModelLocator : BaseViewModelLocator, IViewModelLocator
    {
        public ViewModelLocator(
            IEnvironmentViewModel environmentViewModel,
            IMainViewModel mainViewModel,
            ExemplaryViewModel exemplaryViewModel)
            : base(environmentViewModel)
        {
            MainViewModel = mainViewModel;
            RegisterViewModel(mainViewModel);

            Exemplary = exemplaryViewModel;
            RegisterViewModel(exemplaryViewModel);
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
