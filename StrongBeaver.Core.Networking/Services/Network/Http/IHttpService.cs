using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Network.Http
{
    public interface IHttpService : IService
    {
        Task<IHttpResponse> SendRequestAsync(IHttpRequest request);
    }
}