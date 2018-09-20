using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageMoveFileMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageMoveFileMessage()
        {
            // no operation
        }

        public FileStorageMoveFileMessage(string path, string destinationPath)
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
            OperationHolder = new TaskHolder(service.MoveFileAsync(Path, DestinationPath, Rewrite));
        }
    }
}