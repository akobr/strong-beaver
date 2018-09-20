using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Storage.Files
{
    public interface IFileStorageMessage : IServiceMessage<IFileStorageService>
    {
        FolderTypeEnum RootFolder { get; }

        string Path { get; }
    }
}