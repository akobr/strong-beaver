using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace StrongBeaver.Core.ViewModel
{
    public interface IViewModel : INotifyPropertyChanged, ICleanup, IInitialisable
    {
       // No member
    }
}