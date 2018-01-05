using System.IO;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageOpenFileMessage : ServiceAsyncMesssage<Stream>, IFileStorageMessage
    {
        public FileStorageOpenFileMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
            OnlyRead = true;
            Append = false;
        }

        public FileStorageOpenFileMessage(string filepath, object sender)
            : base(sender)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = filepath;
            OnlyRead = true;
            Append = false;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public bool Append { get; set; }

        public bool OnlyRead { get; set; }

        public void PerformMessage(IFileStorageService service)
        {
            TaskHolder<Stream> result = new TaskHolder<Stream>();
            result.Operation = service.OpenFileAsync(Path, OnlyRead, Append);
            OperationHolder = result;
        }
    }
}