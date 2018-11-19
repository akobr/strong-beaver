using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageExistFileMessage : ServiceAsyncMesssage<bool>, IFileStorageMessage
    {
        public FileStorageExistFileMessage()
        {
            // no operation
        }

        public FileStorageExistFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder<bool>(service.ExistFileAsync(Path));
        }
    }
}