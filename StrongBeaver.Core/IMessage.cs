namespace StrongBeaver.Core
{
    public interface IMessage
    {
        /// <summary>
        /// The message's sender.
        /// This is not a mandatory property, may be null.
        /// </summary>
        object Sender { get; }

        /// <summary>
        /// The message's intended target. This property can be used to
        /// give an indication as to whom the message was intended for. Of course this
        /// is only an indication, and may be null.
        /// </summary>
        object Target { get; }

        /// <summary>
        /// The message's code (string identifier).
        /// This is not a mandatory property, may be empty or null.
        /// </summary>
        string Code { get; }
    }
}
