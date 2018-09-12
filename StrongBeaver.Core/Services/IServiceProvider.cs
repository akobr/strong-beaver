using StrongBeaver.Core.Messaging;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Core.Services
{
    public interface IServiceProvider : IProvider<IService>, IMessageBusProvider
    {
        /// <summary>
        /// The logging service.
        /// Can be null or use static methods on <see cref="StrongBeaver.Core.Provider" /> class which are save.
        /// </summary>
        ILogService Logger { get; }
    }
}