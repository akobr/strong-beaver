using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services.Network.Http;
using StrongBeaver.Core.Services.Network.Http.Headers;

namespace StrongBeaver.Core.Services.Network
{
    public interface IRequest
    {
        Uri Url { get; }

        HttpMethodEnum Method { get; }

        IDictionary<string, IHeader> Headers { get; }

        string UserAgent { get; }

        string AcceptType { get; }

        bool HasContent { get; }

        void SetHeader(IHeader header);
    }
}