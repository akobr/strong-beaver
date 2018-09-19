using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageDeleteFolderMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageDeleteFolderMessage()
        {
            // no operation
        }

        public FileStorageDeleteFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder(service.DeleteFolderAsync(Path));
        }
    }
}