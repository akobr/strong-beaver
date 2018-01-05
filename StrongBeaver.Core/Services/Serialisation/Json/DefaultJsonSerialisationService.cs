using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace StrongBeaver.Core.Services.Serialization.Json
{
    public class DefaultJsonSerialisationService : BaseService, IJsonSerialisationService
    {
        private const int STREAM_BUFFER_SIZE = 1024;

        private readonly JsonSerializerSettings settings;
        private readonly JsonSerializer serializer;

        public DefaultJsonSerialisationService()
        {
            settings = new JsonSerializerSettings();
            serializer = new JsonSerializer();

            PrepareJsonSettings();
        }

        public JsonSerializerSettings SerializerSettings => settings;

        public JsonSerializer Serializer => serializer;

        public void SerializeToStream(JToken jsonData, Stream stream)
        {
            using (StreamWriter txtWriter = CreateStreamWriter(stream))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(txtWriter))
            {
                jsonData.WriteTo(jsonWriter);
            }
        }

        public Task SerializeToStreamAsync(JToken jsonData, Stream stream)
        {
            using (StreamWriter txtWriter = CreateStreamWriter(stream))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(txtWriter))
            {
                return jsonData.WriteToAsync(jsonWriter);
            }
        }

        public void SerializeToStream(object data, Stream stream)
        {
            SerializeObjectIntoStream(data, stream);
        }

        public Task SerializeToStreamAsync(object data, Stream stream)
        {
            return Task.Run(() => SerializeObjectIntoStream(data, stream));
        }

        public JToken DeserializeFromStream(Stream data)
        {
            using (StreamReader txtReader = CreateStreamReader(data))
            using (JsonTextReader jsonReader = new JsonTextReader(txtReader))
            {
                return JToken.ReadFrom(jsonReader);
            }
        }

        public Task<JToken> DeserializeFromStreamAsync(Stream data)
        {
            using (StreamReader txtReader = CreateStreamReader(data))
            using (JsonTextReader jsonReader = new JsonTextReader(txtReader))
            {
                return JToken.ReadFromAsync(jsonReader);
            }
        }

        public TData DeserializeFromStream<TData>(Stream data)
        {
            return DeserializeObjectFromStream<TData>(data);
        }

        public Task<TData> DeserializeFromStreamAsync<TData>(Stream data)
        {
            return Task.Run(() => DeserializeObjectFromStream<TData>(data));
        }

        public string Serialize(object data)
        {
            return SerializeObjectToJson(data);
        }

        public Task<string> SerializeAsync(object data)
        {
            return Task.Run(() => SerializeObjectToJson(data));
        }

        public TData Deserialize<TData>(string jsonData)
        {
            return DeserializeObjectFromJson<TData>(jsonData);
        }

        public Task<TData> DeserializeAsync<TData>(string jsonData)
        {
            return Task.Run(() => DeserializeObjectFromJson<TData>(jsonData));
        }

        #region Non-Public Methods

        private StreamWriter CreateStreamWriter(Stream stream)
        {
            return new StreamWriter(stream, Encoding.UTF8, STREAM_BUFFER_SIZE, true);
        }

        private StreamReader CreateStreamReader(Stream stream)
        {
            return new StreamReader(stream, Encoding.UTF8, false, STREAM_BUFFER_SIZE, true);
        }

        private string SerializeObjectToJson(object data)
        {
            return JsonConvert.SerializeObject(data, settings);
        }

        private void SerializeObjectIntoStream(object data, Stream targetStream)
        {
            using (StreamWriter txtWriter = new StreamWriter(targetStream, Encoding.UTF8, STREAM_BUFFER_SIZE, true))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(txtWriter))
            {
                serializer.Serialize(jsonWriter, data);
            }
        }

        private TData DeserializeObjectFromJson<TData>(string json)
        {
            return JsonConvert.DeserializeObject<TData>(json, settings);
        }

        private TData DeserializeObjectFromStream<TData>(Stream dataStream)
        {
            using (StreamReader txtReader = new StreamReader(dataStream, Encoding.UTF8, false, STREAM_BUFFER_SIZE, true))
            using (JsonTextReader jsonReader = new JsonTextReader(txtReader))
            {
                return serializer.Deserialize<TData>(jsonReader);
            }
        }

        private void PrepareJsonSettings()
        {
            settings.Culture = CultureInfo.InvariantCulture;
            serializer.Culture = CultureInfo.InvariantCulture;

            settings.StringEscapeHandling = StringEscapeHandling.Default;
            serializer.StringEscapeHandling = StringEscapeHandling.Default;

            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateParseHandling = DateParseHandling.DateTimeOffset;
            serializer.DateParseHandling = DateParseHandling.DateTimeOffset;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            serializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            settings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
            serializer.FloatFormatHandling = FloatFormatHandling.DefaultValue;
            settings.FloatParseHandling = FloatParseHandling.Double;
            serializer.FloatParseHandling = FloatParseHandling.Double;

            settings.NullValueHandling = NullValueHandling.Ignore;
            serializer.NullValueHandling = NullValueHandling.Ignore;
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            serializer.MissingMemberHandling = MissingMemberHandling.Ignore;

            settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;
            serializer.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            serializer.ReferenceLoopHandling = ReferenceLoopHandling.Error;
            settings.ObjectCreationHandling = ObjectCreationHandling.Auto;
            serializer.ObjectCreationHandling = ObjectCreationHandling.Auto;

            settings.MaxDepth = 10;
            serializer.MaxDepth = 10;
            settings.CheckAdditionalContent = false;
            serializer.CheckAdditionalContent = false;

            JsonTraceWriter traceWriter = new JsonTraceWriter();
            settings.TraceWriter = traceWriter;
            serializer.TraceWriter = traceWriter;

#if DEBUG
            settings.Formatting = Formatting.Indented;
            serializer.Formatting = Formatting.Indented;
            traceWriter.LevelFilter = TraceLevel.Warning;
#else
            settings.Formatting = Formatting.None;
            serializer.Formatting = Formatting.None;
            traceWriter.LevelFilter = TraceLevel.Error;
#endif
        }

        #endregion
    }
}