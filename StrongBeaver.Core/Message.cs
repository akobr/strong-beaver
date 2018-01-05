using GalaSoft.MvvmLight.Messaging;
using StrongBeaver.Core.Constants;
using System.Text;

namespace StrongBeaver.Core
{
    public class Message : MessageBase, IMessage
    {
        public Message()
            : base()
        {
            // No operation
        }

        public Message(object sender)
            : base(sender)
        {
            // No operation
        }

        public Message(object sender, object target)
            : base(sender, target)
        {
            // No operation
        }

        public Message(object sender, string code)
            : base(sender)
        {
            Code = code;
        }

        public string Code { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Mesage: {GetType().Name}");
            stringBuilder.AppendLine($"Code: {Code ?? GlobalConstatns.DEFAULT_NULL_STRING}");
            stringBuilder.AppendLine($"Source: {Sender?.GetType().Name ?? GlobalConstatns.DEFAULT_NULL_STRING}");
            stringBuilder.Append($"Target: {Target?.GetType().Name ?? GlobalConstatns.DEFAULT_NULL_STRING}");
            return stringBuilder.ToString();
        }
    }
}
