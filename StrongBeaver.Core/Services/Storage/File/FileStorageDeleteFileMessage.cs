using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageDeleteFileMessage : ServiceAsyncMesssage, IFileStorageMessage
    {
        public FileStorageDeleteFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FileStorageDeleteFileMessage(string path, object sender)
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
            result.Operation = service.DeleteFileAsync(Path);
            OperationHolder = result;
        }
    }
}