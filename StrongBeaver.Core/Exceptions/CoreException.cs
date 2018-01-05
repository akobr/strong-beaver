using System;

namespace StrongBeaver.Core.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException()
            : base()
        {
            // No operation
        }

        public CoreException(string message)
            : base(message)
        {
            // No operation
        }

        public CoreException(string message, Exception innerException)
            : base(message, innerException)
        {
            // No operation
        }
    }
}