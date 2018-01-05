using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Core.Services
{
    public interface IServiceProvider : IProvider<IService>, IMessageBusProvider
    {
        /// <summary>
        /// The logging service.
        /// The service can be used directly, but can be null or we recommend to use static and save methods on <see cref="StrongBeaver.Core.Provider" /> class.
        /// </summary>
        ILogService Logger { get; }
    }
}