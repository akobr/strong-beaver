using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StrongBeaver.Core.Services.Serialisation.Json
{
    public interface IJsonSerialisationService : IJsonSerialisationSyncService, IJsonSerialisationAsyncService, ISerialisationService
    {
        // No member
    }

    public interface IJsonSerialisationSyncService : ISerialisationSyncService
    {
        JsonSerializerSettings SerializerSettings { get; }

        JsonSerializer Serializer { get; }

        void SerializeToStream(JToken jsonData, Stream stream);

        JToken DeserializeFromStream(Stream data);
    }

    public interface IJsonSerialisationAsyncService : ISerialisationAsyncService
    {
        JsonSerializerSettings SerializerSettings { get; }

        JsonSerializer Serializer { get; }

        Task SerializeToStreamAsync(JToken jsonData, Stream stream);

        Task<JToken> DeserializeFromStreamAsync(Stream data);
    }
}