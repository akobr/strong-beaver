namespace StrongBeaver.Core.Services.Storage.File
{
    public interface IFileStorageMessage : IServiceMessage
    {
        FolderTypeEnum RootFolder { get; }

        string Path { get; }

        void PerformMessage(IFileStorageService service);
    }
}