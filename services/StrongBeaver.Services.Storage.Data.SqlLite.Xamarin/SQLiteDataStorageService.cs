using StrongBeaver.Core.Services;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Services.Storage.Data
{
    public class SqLiteDataStorageService : BaseDisposableService, IDataStorageService
    {
        private readonly ISqlContext sqlContext;
        private readonly IDataBuilder dataBuilder;
        private readonly ILogService logging;

        public SqLiteDataStorageService(ISqlContextFactory contextFactory, IDataBuilderFactory builderFactory, ILogService logging)
        {
            this.logging = logging;
            sqlContext = contextFactory.Create();
            dataBuilder = builderFactory.Create(sqlContext);
        }

        public IDataBuilder GetBuilder()
        {
            return dataBuilder;
        }

        public IAsyncDataOperation GetAsyncOperation()
        {
            return new AsyncDataOperation(sqlContext, logging);
        }

        public IDataOperation GetOperation()
        {
            return new DataOperation(sqlContext, logging);
        }
    }
}
