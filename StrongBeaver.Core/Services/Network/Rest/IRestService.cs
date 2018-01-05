using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Network.Rest
{
    public interface IRestService : IService
    {
        Task<IRestResponse<TData>> SendRequestAsync<TData>(IRestRequest request);
    }
}