using System;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Network.Transfer
{
    public interface ITransferService
    {
        Task DownloadFileAsync(Uri url, string filePath);

        Task<string> DownloadFileTemporaryAsync(Uri url);

        Task UploadFileAsync(Uri ulr, string filePath);
    }
}