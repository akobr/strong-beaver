using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Dialog
{
    public interface IDialogService : IService, GalaSoft.MvvmLight.Views.IDialogService, IMessageBusService<IDialogMessage>
    {
        void Configure(string dialogKey, Type dialogType);

        Task ShowDialogByNameAsync(string dialogName);

        Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions);

        void ShowToast(string text);

        void ShowToast(IToast toast);
    }
}