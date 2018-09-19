using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Dialog
{
    public interface IDialogService : IMessageBusService<IDialogMessage>
    {
        void Configure(string dialogKey, Type dialogType);

        Task ShowDialogAsync(string dialogKey);

        Task ShowDialogAsync(Type dialogType);

        Task ShowDialogAsync(string title, string message);

        Task<int> ShowDialogAsync(string title, string message, IReadOnlyCollection<IDialogActionDescription> actions);
    }
}