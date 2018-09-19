using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageCreateFolderMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageCreateFolderMessage()
        {
            // no operation
        }

        public FileStorageCreateFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder(service.CreateFolderAsync(Path));
        }
    }
}