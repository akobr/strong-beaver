using System;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Navigation
{
    public class NavigateToUriMessage : ServiceAsyncMesssage<bool>, INavigationMessage
    {
        public NavigateToUriMessage(Uri targetUri)
        {
            TargetUri = targetUri;
        }

        public NavigateToUriMessage(object sender, Uri targetUri)
            : base(sender)
        {
            TargetUri = targetUri;
        }

        public Uri TargetUri { get; }

        public void PerformMessage(INavigationService service)
        {
            OperationHolder = new TaskHolder<bool>(service.NavigateToUriAsync(TargetUri));
        }
    }
}
