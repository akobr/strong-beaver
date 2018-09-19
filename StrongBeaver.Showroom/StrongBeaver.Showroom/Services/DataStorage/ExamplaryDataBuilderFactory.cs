using StrongBeaver.Services.Storage.Data;

namespace StrongBeaver.Showroom.Services.DataStorage
{
    public class ExamplaryDataBuilderFactory : IDataBuilderFactory
    {
        public IDataBuilder Create(ISqlContext context)
        {
            return new ExemplaryDataBuilder(context);
        }
    }
}
