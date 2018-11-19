using System;
using System.Collections.Generic;
using System.IO;
using StrongBeaver.Core.Services.Network.Http.Headers;
using StrongBeaver.Core.Services.Network.Rest;

namespace StrongBeaver.Core.Services.Network.Http
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest()
        {
            Headers = new Dictionary<string, IHeader>();
        }

        public HttpRequest(string url)
            : this()
        {
            Url = new Uri(url, UriKind.Absolute);
        }

        internal HttpRequest(IRestRequest restRequest, Stream contentStream)
        {
            Url = restRequest.Url;
            Headers = restRequest.Headers;
            Method = restRequest.Method;
            UserAgent = restRequest.UserAgent;
            AcceptType = restRequest.AcceptType;
            Content = contentStream;
            ContentType = HttpContants.CONTENT_TYPE_JSON;
        }

        public Uri Url { get; set; }

        public HttpMethodEnum Method { get; set; }

        public IDictionary<string, IHeader> Headers { get; }

        public string UserAgent { get; set; }

        public string AcceptType { get; set; }

        public string ContentType { get; set; }

        public Stream Content { get; set; }

        public bool HasContent => Content != null && Content != Stream.Null;

        public void SetHeader(IHeader header)
        {
            if (header == null)
            {
                return;
            }

            Headers[header.Name] = header;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Content?.Dispose();
            Content = null;
        }
    }
}