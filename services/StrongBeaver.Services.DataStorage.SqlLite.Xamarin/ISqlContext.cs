using SQLite.Net.Interop;

namespace StrongBeaver.Core.Services.Storage.Data
{
    public interface ISqlContext
    {
        ISQLitePlatform SqlPlatform { get; }

        string DatabasePath { get; }
    }
}
