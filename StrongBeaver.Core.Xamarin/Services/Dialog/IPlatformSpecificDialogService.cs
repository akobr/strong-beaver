using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Dialog
{
    public interface IPlatformSpecificDialogService
    {
        void ShowToast(IToast toast);

        Task ShowMessage(string message, string title);

        Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions);
    }
}