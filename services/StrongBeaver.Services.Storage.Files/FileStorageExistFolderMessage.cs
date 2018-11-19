using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageExistFolderMessage : ServiceAsyncMesssage<bool>, IFileStorageMessage
    {
        public FileStorageExistFolderMessage()
        {
            // no operation
        }

        public FileStorageExistFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set;  }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder<bool>(service.ExistFolderAsync(Path));
        }
    }
}