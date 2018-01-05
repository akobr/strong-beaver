using System;
using System.IO;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Network.Http
{
    public interface IHttpResponse : IResponse, IDisposable
    {
        Task<Stream> GetContentStreamAsync();

        Task<string> GetContentAsStringAsync();
    }
}