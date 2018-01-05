using System.IO;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Serialization
{
    public interface ISerialisationService
        : ISerialisationSyncService, ISerialisationAsyncService
    {
        // No member
    }

    public interface ISerialisationSyncService : IService
    {
        string Serialize(object data);

        TData Deserialize<TData>(string stringData);

        void SerializeToStream(object data, Stream stream);

        TData DeserializeFromStream<TData>(Stream data);
    }

    public interface ISerialisationAsyncService : IService
    {
        Task<string> SerializeAsync(object data);

        Task<TData> DeserializeAsync<TData>(string stringData);

        Task SerializeToStreamAsync(object data, Stream stream);

        Task<TData> DeserializeFromStreamAsync<TData>(Stream data);
    }
}