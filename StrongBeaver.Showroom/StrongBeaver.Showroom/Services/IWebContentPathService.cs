using StrongBeaver.Core.Services;

namespace StrongBeaver.Showroom.Services
{
    public interface IWebContentPathService : IService
    {
        string GetPath();
    }
}
