using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Toast
{
    public interface IToastService : IService
    {
        void ShowToast(string text);

        void ShowToast(IToast toast);
    }
}