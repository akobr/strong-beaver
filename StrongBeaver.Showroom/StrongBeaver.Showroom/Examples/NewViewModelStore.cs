using StrongBeaver.Core;
using StrongBeaver.Core.Model;

namespace StrongBeaver.Showroom.Examples
{
    public class NewViewModelStore : ViewObjectStore<int, NewObservableItem>
    {
        // Create a new store class isn't necessary

        public NewViewModelStore()
            : base((item) => item.Id) // Factory for item key
        {
            // No operation
        }
    }

    public class NewObservableItem : ObservableObject, IViewObject<NewObservableItem>
    {
        // The base class ObservableObject is only recomendation for databinding,
        // mandatory is only IViewObject interface

        // Item key property
        public int Id { get; set; }

        public void Update(NewObservableItem newItem)
        {
            // Update item with new content
        }

        public void Initialise()
        {
            // Implement item initialisation
            // (calculate view properties)
        }

        public void Dispose()
        {
            // Dospose item
        }
    }
}
