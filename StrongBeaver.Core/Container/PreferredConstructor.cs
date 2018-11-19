using System;

namespace StrongBeaver.Core.Container
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class PreferredConstructorAttribute : Attribute
    {
        // no member
    }
}