using GalaSoft.MvvmLight;

namespace StrongBeaver.Core.ViewModel
{
    public abstract class BaseViewModel : ViewModelBase, IViewModel
    {
        public virtual void Initialise()
        {
            // No operation ( template method )
        }
    }
}