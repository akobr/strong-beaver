using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public class ViewModelProvider : IViewModelProvider
    {
        public ViewModelProvider(IMainViewModel main, ExemplaryViewModel exemplary, IEnvironmentViewModel environment)
        {
            Main = main;
            Exemplary = exemplary;
            Environment = environment;
        }

        public IMainViewModel Main { get; }

        public ExemplaryViewModel Exemplary { get; }

        public IEnvironmentViewModel Environment { get; }
    }
}
