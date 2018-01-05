using System;
using System.Collections.Generic;
using System.Globalization;
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

        public void PrepareUrl(string urlFormat, params object[] urlArguments)
        {
            Url = new Uri(string.Format(CultureInfo.InvariantCulture, urlFormat, EncodeArguments(urlArguments)));
        }

        public void SetHeader(IHeader header)
        {
            if (header == null)
            {
                return;
            }

            Headers[header.Name] = header;
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
    }
}