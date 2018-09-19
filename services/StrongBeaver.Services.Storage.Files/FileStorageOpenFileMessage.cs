using System.IO;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageOpenFileMessage : ServiceAsyncMesssage<Stream>, IFileStorageMessage
    {
        public FileStorageOpenFileMessage()
        {
            // no operation
        }

        public FileStorageOpenFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
            OnlyRead = true;
            Append = false;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public bool Append { get; set; }

        public bool OnlyRead { get; set; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder<Stream>(service.OpenFileAsync(Path, OnlyRead, Append));
        }
    }
}