using System;
using System.ComponentModel;

namespace StrongBeaver.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentSyntax
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();

        [EditorBrowsable(EditorBrowsableState.Never)]
        string ToString();

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object obj);
    }
}