using System;
using System.Collections.Generic;
using System.IO;

namespace StrongBeaver.Core.Services.Network.Http
{
    public interface IHttpRequest : IRequest, IDisposable
    {
        string ContentType { get; }

        Stream Content { get; }
    }
}