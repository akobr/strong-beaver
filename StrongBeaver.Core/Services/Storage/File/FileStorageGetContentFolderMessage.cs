using System.Collections.Generic;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Core.Services.Storage.File
{
    public class FileStorageGetContentFolderMessage : ServiceAsyncMesssage<ICollection<string>>, IFileStorageMessage
    {
        public FileStorageGetContentFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FileStorageGetContentFolderMessage(string path, object sender)
            : base(sender)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            TaskHolder<ICollection<string>> result = new TaskHolder<ICollection<string>>();
            result.Operation = service.GetFolderContentAsync(Path);
            OperationHolder = result;
        }
    }
}