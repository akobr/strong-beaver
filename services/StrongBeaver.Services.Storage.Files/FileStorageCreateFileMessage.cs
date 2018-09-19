using System.IO;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageCreateFileMessage : ServiceAsyncMesssage<Stream>, IFileStorageMessage
    {
        public FileStorageCreateFileMessage()
        {
            // no operation
        }

        public FileStorageCreateFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
            Rewrite = true;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public bool Rewrite { get; set; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder<Stream>(service.CreateFileAsync(Path, Rewrite));
        }
    }
}