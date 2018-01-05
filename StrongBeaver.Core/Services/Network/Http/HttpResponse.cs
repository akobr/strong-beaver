using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Network.Http
{
    public class HttpResponse : IHttpResponse
    {
        private readonly HttpResponseMessage responseMessage;

        public HttpResponse(HttpResponseMessage responseMessage)
        {
            this.responseMessage = responseMessage;
        }

        public HttpResponse(Exception exception)
        {
            Exception = exception;
        }

        public Uri Url => responseMessage?.RequestMessage.RequestUri;

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers
            => responseMessage?.Headers ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>();

        public HttpStatusCodeEnum StatusCode => (HttpStatusCodeEnum)(responseMessage?.StatusCode ?? 0);

        public string ReasonPhrase => responseMessage?.ReasonPhrase;

        public Exception Exception { get; }

        public bool HasException => Exception != null;

        public bool IsSuccess => responseMessage != null && responseMessage.IsSuccessStatusCode;

        public async Task<Stream> GetContentStreamAsync()
        {
            if (responseMessage == null)
            {
                return null;
            }

            return (await responseMessage.Content.ReadAsStreamAsync());
        }

        public Task<string> GetContentAsStringAsync()
        {
            if (responseMessage == null)
            {
                return Task.FromResult<string>(null);
            }

            return responseMessage.Content.ReadAsStringAsync();
        }

        public IDictionary<string, IEnumerable<string>> GetHeadersAsDictionary()
        {
            if (responseMessage == null)
            {
                return null;
            }

            IDictionary<string, IEnumerable<string>> result = new Dictionary<string, IEnumerable<string>>();

            foreach (var keyValuePair in Headers)
            {
                result.Add(keyValuePair);
            }

            return result;
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

            responseMessage?.Dispose();
        }
    }
}