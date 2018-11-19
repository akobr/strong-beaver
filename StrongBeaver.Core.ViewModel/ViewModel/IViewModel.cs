using System.ComponentModel;

namespace StrongBeaver.Core.ViewModel
{
    public interface IViewModel : INotifyPropertyChanged, ICleanup, IInitialisable
    {
       // No member
    }
}