using StrongBeaver.Core.Services.Storage.Data;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using System.IO;

namespace StrongBeaver.Showroom.iOS.Services.DataStorage
{
    public class IosSQLiteContext : ISqlContext
    {
        public IosSQLiteContext()
        {
            SqlPlatform = new SQLitePlatformIOS();

            string personalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
             DatabasePath = Path.Combine(personalPath, "exemplary-db");
        }

        public ISQLitePlatform SqlPlatform { get; }

        public string DatabasePath { get; }
    }
}