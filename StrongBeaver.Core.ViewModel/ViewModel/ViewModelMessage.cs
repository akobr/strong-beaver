using StrongBeaver.Core.Messaging;

namespace StrongBeaver.Core.ViewModel
{
    public class ViewModelMessage : Message, IViewModelMessage
    {
        public ViewModelMessage()
        {
            // No operation
        }

        public ViewModelMessage(object sender)
            : base(sender)
        {
            // Empty
        }

        public ViewModelMessage(object sender, object target)
            : base(sender, target)
        {
            // Empty
        }

        public ViewModelMessage(object sender, string code)
            : base(sender, code)
        {
            // No operation
        }
    }
}
