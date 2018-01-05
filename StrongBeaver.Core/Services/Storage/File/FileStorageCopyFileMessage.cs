using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageCopyFileMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageCopyFileMessage(string path, string destinationPath)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
            DestinationPath = destinationPath;
            Rewrite = true;
        }

        public FileStorageCopyFileMessage(string path, string destinationPath, object sender)
            : base(sender)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
            DestinationPath = destinationPath;
            Rewrite = true;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public string DestinationPath { get; }

        public bool Rewrite { get; set; }

        public void PerformMessage(IFileStorageService service)
        {
            TaskHolder result = new TaskHolder();
            result.Operation = service.CopyFileAsync(Path, DestinationPath, Rewrite);
            OperationHolder = result;
        }
    }
}
