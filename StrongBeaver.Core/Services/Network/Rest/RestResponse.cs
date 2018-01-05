using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services.Network.Http;

namespace StrongBeaver.Core.Services.Network.Rest
{
    public class RestResponse<TData> : IRestResponse<TData>
    {
        public RestResponse(Uri url, Exception exception)
        {
            Url = url;
            IsSuccess = false;
            Exception = exception;

            ReasonPhrase = Exception.Message;
            StatusCode = HttpStatusCodeEnum.None;
            Headers = new Dictionary<string, IEnumerable<string>>();
            Content = default(TData);
        }

        public RestResponse(IHttpResponse httpResponse)
        {
            Url = httpResponse.Url;
            Headers = httpResponse.Headers;
            StatusCode = httpResponse.StatusCode;
            ReasonPhrase = httpResponse.ReasonPhrase;
            Exception = httpResponse.Exception;
            IsSuccess = httpResponse.IsSuccess;

            Content = default(TData);
        }

        public Uri Url { get; set; }

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers { get; set; }

        public HttpStatusCodeEnum StatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public Exception Exception { get; set; }

        public bool HasException => Exception != null;

        public bool IsSuccess { get; set; }

        public TData Content { get; set; }

        public IDictionary<string, IEnumerable<string>> GetHeadersAsDictionary()
        {
            IDictionary<string, IEnumerable<string>> result = new Dictionary<string, IEnumerable<string>>();

            foreach (var keyValuePair in Headers)
            {
                result.Add(keyValuePair);
            }

            return result;
        }
    }
}