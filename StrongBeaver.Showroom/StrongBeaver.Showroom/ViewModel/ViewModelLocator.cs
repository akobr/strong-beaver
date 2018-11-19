using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Core.ViewModel;

namespace StrongBeaver.Showroom.ViewModel
{
    public class ViewModelLocator : BaseViewModelLocator, IViewModelLocator
    {
        public IMainViewModel Main { get; private set; }

        public ExemplaryViewModel Exemplary { get; private set; }

        public void Startup(ILogService logging, IViewModelProvider modelProvider)
        {
            Startup(logging, modelProvider.Environment);

            Main = modelProvider.Main;
            RegisterViewModel(Main);

            Exemplary = modelProvider.Exemplary;
            RegisterViewModel(Exemplary);
        }
    }
}
