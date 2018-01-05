using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Cleanup;

namespace StrongBeaver.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void RemoveFromIoc<TClass>(this TClass instance)
            where TClass : class
        {
            if (instance == null)
            {
                return;
            }

            IIocCleanupService cleanupService = ServiceProvider.Current.Get<IIocCleanupService>();

            if (cleanupService == null)
            {
                return;
            }

            cleanupService.RemoveInstance(instance);
        }
    }
}
