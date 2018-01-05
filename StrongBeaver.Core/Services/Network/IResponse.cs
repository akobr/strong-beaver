using System;
using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Network.Http
{
    public interface IResponse
    {
        Uri Url { get; }

        IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; }

        HttpStatusCodeEnum StatusCode { get; }

        string ReasonPhrase { get; }

        Exception Exception { get; }

        bool HasException { get; }

        bool IsSuccess { get; }

        IDictionary<string, IEnumerable<string>> GetHeadersAsDictionary();
    }
}