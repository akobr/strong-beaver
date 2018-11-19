namespace StrongBeaver.Core.ViewModel
{
    public abstract class BaseViewModel : ObservableObject, IViewModel
    {
        public virtual void Initialise()
        {
            // No operation ( template method )
        }

        public virtual void Cleanup()
        {
            // No operation ( template method )
        }
    }
}