using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageExistFileMessage : ServiceAsyncMesssage<bool>, IFileStorageMessage
    {
         public FileStorageExistFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

         public FileStorageExistFileMessage(string path, object sender)
            : base(sender)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            TaskHolder<bool> result = new TaskHolder<bool>();
            result.Operation = service.ExistFileAsync(Path);
            OperationHolder = result;
        }
    }
}