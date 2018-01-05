using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void PrepareUrl(string urlFormat, params object[] urlArguments)
        {
            Url = new Uri(string.Format(
                urlFormat, CultureInfo.InvariantCulture, EncodeArguments(urlArguments)),
                UriKind.Absolute);
        }

        // TODO: [P3] Transfet to helper method
        private static object[] EncodeArguments(object[] arguments)
        {
            if (arguments == null || arguments.Length < 1)
            {
                return new object[0];
            }

            object[] result = new object[arguments.Length];

            for (int i = 0; i < arguments.Length; i++)
            {
                object argument = arguments[i];
                IFormattable formattableArgument = argument as IFormattable;

                string stringifiedArgument = formattableArgument == null
                    ? argument.ToString()
                    : formattableArgument.ToString(null, CultureInfo.InvariantCulture);

                result[i] = Uri.EscapeDataString(stringifiedArgument);
            }

            return result;
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