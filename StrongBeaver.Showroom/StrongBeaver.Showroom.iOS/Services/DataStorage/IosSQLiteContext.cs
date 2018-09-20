using StrongBeaver.Core.Services.Storage.Data;
using System.IO;

namespace StrongBeaver.Showroom.iOS.Services.DataStorage
{
    public class IosSqLiteContext : ISqlContext
    {
        public IosSqLiteContext()
        {
            string personalPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
             DatabasePath = Path.Combine(personalPath, "exemplary-db");
        }

        public string DatabasePath { get; }
    }
}