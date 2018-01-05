using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Navigation
{
    public interface ISystemNavigationService
    {
        Task<bool> NavigateToAsync(SystemDestinationEnum destination);
    }
}
