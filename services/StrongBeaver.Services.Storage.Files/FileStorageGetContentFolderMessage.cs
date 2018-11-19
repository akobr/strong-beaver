using System.Collections.Generic;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Storage.Files
{
    public class FileStorageGetContentFolderMessage : ServiceAsyncMesssage<ICollection<string>>, IFileStorageMessage
    {
        public FileStorageGetContentFolderMessage()
        {
            // no operation
        }

        public FileStorageGetContentFolderMessage(string path)
        {
            RootFolder = FolderTypeEnum.Application;
            Path = path;
        }

        public FolderTypeEnum RootFolder { get; set; }

        public string Path { get; }

        public void PerformMessage(IFileStorageService service)
        {
            OperationHolder = new TaskHolder<ICollection<string>>(service.GetFolderContentAsync(Path));
        }
    }
}