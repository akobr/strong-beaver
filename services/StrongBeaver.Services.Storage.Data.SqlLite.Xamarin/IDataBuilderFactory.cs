using StrongBeaver.Core;

namespace StrongBeaver.Services.Storage.Data
{
    public interface IDataBuilderFactory : IFactory<IDataBuilder, ISqlContext>
    {
        // no operation
    }
}