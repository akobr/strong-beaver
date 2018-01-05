using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using StrongBeaver.Core.Services.Network.Http.Headers;

namespace StrongBeaver.Core.Services.Network.Http
{
    public class DefaultHttpService : BaseService, IHttpService
    {
        public Task<IHttpResponse> SendRequestAsync(IHttpRequest request)
        {
            return SendRequestAsync(request, new HttpMethod(request.Method.ToString()));
        }

        private static async Task<IHttpResponse> SendRequestAsync(IHttpRequest request, HttpMethod method)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Provider.LogTraceMessage($"Sending {method} HTTP request to '{request.Url}'");

                    using (HttpRequestMessage requestMessage = PrepareHttpRequestMessage(request, method))
                    {
                        HttpResponseMessage responseMessage = await client.SendAsync(requestMessage);
                        return new HttpResponse(responseMessage);
                    }
                }
            }
            catch (Exception exception)
            {
                Provider.LogErrorMessage(
                    $"HTTP; Url: {request.Url}; METHOD: {method}",
                    "Error occured during HTTP communication.",
                    exception, request, method);
                return new HttpResponse(exception);
            }
        }

        private static HttpRequestMessage PrepareHttpRequestMessage(IHttpRequest request, HttpMethod method)
        {
            HttpRequestMessage message = new HttpRequestMessage(method, request.Url);

            AddUserAgentHeader(request, message);
            AddAcceptTypeHeader(request, message);
            AddGenericHeaders(request, message);
            AddContent(request, message);

            return message;
        }

        private static void AddUserAgentHeader(IHttpRequest request, HttpRequestMessage message)
        {
            if (string.IsNullOrEmpty(request.UserAgent))
            {
                return;
            }

            message.Headers.UserAgent.ParseAdd(request.UserAgent);
        }

        private static void AddAcceptTypeHeader(IHttpRequest request, HttpRequestMessage message)
        {
            if (string.IsNullOrEmpty(request.AcceptType))
            {
                return;
            }

            message.Headers.Accept.ParseAdd(request.AcceptType);
        }

        private static void AddGenericHeaders(IHttpRequest request, HttpRequestMessage message)
        {
            foreach (IHeader header in request.Headers.Values)
            {
                message.Headers.Add(header.Name, header.Value);
            }
        }

        private static void AddContent(IHttpRequest request, HttpRequestMessage message)
        {
            if (!request.HasContent)
            {
                return;
            }

            message.Content = new StreamContent(request.Content);

            string contentType = string.IsNullOrEmpty(request.ContentType)
                ? HttpContants.CONTENT_TYPE_JSON
                : request.ContentType;

            message.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        }
    }
}