using StrongBeaver.Core.Model;

namespace StrongBeaver.Showroom.Model
{
    public class ExemplaryStore : ComplexStore<int, ExemplaryItem>
    {
        public ExemplaryStore()
            : base((item) => item.Id)
        {
        }
    }
}
