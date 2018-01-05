using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageDeleteFolderMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageDeleteFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FileStorageDeleteFolderMessage(string path, object sender)
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
            result.Operation = service.DeleteFolderAsync(Path);
            OperationHolder = result;
        }
    }
}