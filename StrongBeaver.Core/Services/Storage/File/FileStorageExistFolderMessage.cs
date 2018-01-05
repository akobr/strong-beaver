using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageExistFolderMessage : ServiceAsyncMesssage<bool>, IFileStorageMessage
    {
        public FileStorageExistFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FileStorageExistFolderMessage(string path, object sender)
            : base(sender)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set;  }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            TaskHolder<bool> result = new TaskHolder<bool>();
            result.Operation = service.ExistFolderAsync(Path);
            OperationHolder = result;
        }
    }
}