using StrongBeaver.Core.Container;

namespace StrongBeaver.Core.Model
{
    public class StoreProvider : Provider<IStore>, IStoreProvider
    {
        public StoreProvider(IContainer container)
            : base(container)
        {
            Provider.LogDebugMessage($"The Store provider ''{GetType().Name}'' has been created.");
        }

        public static IStoreProvider Current { get; private set; }

        public static void SetCurrentProvider(IStoreProvider storeProvider)
        {
            Current = storeProvider;
        }
    }
}
