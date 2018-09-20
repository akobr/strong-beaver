using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services.Network.Http;
using StrongBeaver.Core.Services.Network.Http.Headers;

namespace StrongBeaver.Core.Services.Network.Rest
{
    public class RestRequest : IRestRequest
    {
        public RestRequest()
        {
            Headers = new Dictionary<string, IHeader>();
        }

        public RestRequest(string url)
            : this(new Uri(url, UriKind.Absolute))
        {
            // Empty
        }

        public RestRequest(Uri url)
            : this()
        {
            Url = url;
        }

        public RestRequest(string url, object content)
            : this(new Uri(url, UriKind.Absolute))
        {
            Content = content;
        }

        public RestRequest(Uri url, object content)
            : this(url)
        {
            Content = content;
        }

        public Uri Url { get; set; }

        public HttpMethodEnum Method { get; set; }

        public IDictionary<string, IHeader> Headers { get; }

        public string UserAgent { get; set; }

        public string AcceptType { get; set; }

        public object Content { get; set; }

        public bool HasContent => Content != null;

        public void SetHeader(IHeader header)
        {
            if (header == null)
            {
                return;
            }

            Headers[header.Name] = header;
        }
    }
}