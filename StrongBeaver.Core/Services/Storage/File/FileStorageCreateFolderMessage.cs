using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageCreateFolderMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageCreateFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FileStorageCreateFolderMessage(string path, object sender)
            : base(sender)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            TaskHolder result = new TaskHolder();
            result.Operation = service.CreateFolderAsync(Path);
            OperationHolder = result;
        }
    }
}