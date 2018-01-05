using System.IO;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageCreateFileMessage : ServiceAsyncMesssage<Stream>, IFileStorageMessage
    {
        public FileStorageCreateFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
            Rewrite = true;
        }

        public FileStorageCreateFileMessage(string path, object sender)
            : base(sender)
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
            TaskHolder<Stream> result = new TaskHolder<Stream>();
            result.Operation = service.CreateFileAsync(Path, Rewrite);
            OperationHolder = result;
        }
    }
}