using StrongBeaver.Core.Model;

namespace StrongBeaver.Showroom.Examples
{
    public class NewItem
    {
        public int Id { get; }
    }

    public class NewStore : SimpleStore<int, NewItem>
    {
        // Create a new store class only if you need some extra logic
        // for it or simply use the class SimpleStore directly

        public NewStore()
         : base((item) => item.Id) // Item key factory
        {
           // No operation
        }
    }
}
