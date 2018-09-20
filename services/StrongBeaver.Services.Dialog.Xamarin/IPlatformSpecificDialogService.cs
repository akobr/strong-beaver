using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrongBeaver.Services.Dialog
{
    public interface IPlatformSpecificDialogService
    {
        Task ShowDialogAsync(string title, string message);

        Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions);
    }
}