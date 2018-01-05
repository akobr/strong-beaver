using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Network.Http.Headers
{
    public interface IHeader
    {
        string Name { get; }

        IEnumerable<string> Value { get; }
    }
}