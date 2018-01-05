using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Storage.File
{
    public interface IFileStorageService : IService, IMessageBusService<IFileStorageMessage>
    {
        string GetFullPath(string filePath);

        Task<Stream> CreateFileAsync(string filePath, bool rewrite);

        Task<Stream> OpenFileAsync(string filePath, bool onlyRead, bool append);

        Task<bool> ExistFileAsync(string filePath);

        Task DeleteFileAsync(string filePath);

        Task CopyFileAsync(string sourceFilePath, string destinationFilePath, bool rewrite);

        Task MoveFileAsync(string sourceFilePath, string destinationFilePath, bool rewrite);

        Task CreateFolderAsync(string folderPath);

        Task<ICollection<string>> GetFolderContentAsync(string folderPath);

        Task<bool> ExistFolderAsync(string folderPath);

        Task DeleteFolderAsync(string folderPath);
    }
}