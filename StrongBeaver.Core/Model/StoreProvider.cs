using GalaSoft.MvvmLight.Ioc;

namespace StrongBeaver.Core.Model
{
    public class StoreProvider : Provider<IStore>, IStoreProvider
    {
        public StoreProvider(ISimpleIoc container)
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
