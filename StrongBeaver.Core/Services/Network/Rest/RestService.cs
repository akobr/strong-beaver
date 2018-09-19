using System;
using System.IO;
using System.Threading.Tasks;
using StrongBeaver.Core.Services.Logging;
using StrongBeaver.Core.Services.Network.Http;
using StrongBeaver.Core.Services.Serialisation;

namespace StrongBeaver.Core.Services.Network.Rest
{
    public class RestService : IRestService
    {
        private readonly ILogService logging;
        private readonly IHttpService http;
        private readonly ISerialisationService serialisation;

        public RestService(ILogService logging, ISerialisationService serialisation, IHttpService http)
        {
            this.logging = logging;
            this.serialisation = serialisation;
            this.http = http;
        }

        public async Task<IRestResponse<TData>> SendRequestAsync<TData>(IRestRequest request)
        {
            MemoryStream requestStream = null;

            try
            {
                if (request.HasContent)
                {
                    requestStream = new MemoryStream();
                    await serialisation.SerializeToStreamAsync(request.Content, requestStream);
                    requestStream.Position = 0;

                }

                using (IHttpRequest httpRequest = new HttpRequest(request, requestStream))
                using (IHttpResponse httpResponse = await http.SendRequestAsync(httpRequest))
                {
                    return await PrepareResponse<TData>(httpRequest, httpResponse);
                }
            }
            catch (Exception ex)
            {
                return new RestResponse<TData>(request.Url, ex);
            }
            finally
            {
                requestStream?.Dispose();
            }
        }

        #region Non-Public Methods

        private async Task<IRestResponse<TData>> PrepareResponse<TData>(IHttpRequest httpRequest, IHttpResponse httpResponse)
        {
            RestResponse<TData> result = new RestResponse<TData>(httpResponse);

            logging.Trace($"Processing REST response with status code {result.StatusCode}.", httpResponse.Url, httpResponse.ReasonPhrase);

            if (!result.IsSuccess)
            {
                return result;
            }

            try
            {
                Stream responseStream = await httpResponse.GetContentStreamAsync();
                result.Content = await serialisation.DeserializeFromStreamAsync<TData>(responseStream);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;

                logging.Error(
                    $"REST; Url: {result.Url}; METHOD: {httpRequest.Method}; CODE: {result.StatusCode}",
                    "Error during de-serializing response content.", ex, httpResponse);
            }

            return result;
        }

        #endregion
    }
}