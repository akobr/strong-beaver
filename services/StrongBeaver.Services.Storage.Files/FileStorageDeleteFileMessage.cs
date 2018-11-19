using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageDeleteFileMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageDeleteFileMessage()
        {
            // no operation
        }

        public FileStorageDeleteFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder(service.DeleteFileAsync(Path));
        }
    }
}