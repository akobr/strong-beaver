using StrongBeaver.Core.Services;

namespace StrongBeaver.Showroom.Examples
{
    public class NewFirstService : BaseDisposableService
    {
        // Implement your new service

        protected override void OnDispose(bool disposing)
        {
            // Implement overrided method or delete
        }
    }

    // OR

    public class NewSecondService : IService
    {
        // Implement your new service

        public void Dispose()
        {
            // Implement Dispose pattern by your own
        }
    }
}
