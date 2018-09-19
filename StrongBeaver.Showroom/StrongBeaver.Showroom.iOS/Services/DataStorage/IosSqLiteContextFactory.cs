using StrongBeaver.Services.Storage.Data;

namespace StrongBeaver.Showroom.iOS.Services.DataStorage
{
    public class IosSqLiteContextFactory : ISqlContextFactory
    {
        public ISqlContext Create()
        {
            return new IosSqLiteContext();
        }
    }
}