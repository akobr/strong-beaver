using System.IO;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Extensions
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Task<Stream> ToStreamAsync(this string text)
        {
            return Task.Run(() => ToStream(text));
        }
    }
}