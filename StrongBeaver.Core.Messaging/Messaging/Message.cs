using System;
using System.Text;

namespace StrongBeaver.Core.Messaging
{
    public class Message : IMessage
    {
        public Message()
        {
            // no operation
        }

        public Message(object sender)
        {
            Sender = sender;
        }

        public Message(object sender, object target)
        {
            Sender = sender;
            Target = target;
        }

        public Message(object sender, string code)
        {
            Sender = sender;
            Code = code;
        }

        public string Code { get; set; }

        public object Sender { get; set; }

        public object Target { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Mesage: {GetType().Name}");

            if (!string.IsNullOrEmpty(Code))
            {
                stringBuilder.AppendLine($"Code: {Code}");
            }

            if (Sender != null)
            {
                stringBuilder.AppendLine($"Sender: {Sender.GetType().Name}");
            }

            if (Target != null)
            {
                stringBuilder.AppendLine($"Target: {Target?.GetType().Name}");
            }

            stringBuilder.Remove(stringBuilder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
            return stringBuilder.ToString();
        }
    }
}