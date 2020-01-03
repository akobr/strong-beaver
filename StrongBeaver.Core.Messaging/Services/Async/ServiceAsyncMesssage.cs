namespace StrongBeaver.Core.Services.Async
{
    public class ServiceAsyncMesssage : ServiceMessage, IServiceAsyncMesssage
    {
        public ServiceAsyncMesssage()
        {
            OperationHolder = new TaskHolder();
        }

        public ServiceAsyncMesssage(object sender)
            : base(sender)
        {
            OperationHolder = new TaskHolder();
        }

        public ServiceAsyncMesssage(object sender, object target)
            : base(sender, target)
        {
            OperationHolder = new TaskHolder();
        }

        public ITaskHolder OperationHolder { get; protected set; }
    }

    public class ServiceAsyncMesssage<T> : ServiceMessage
    {
        public ServiceAsyncMesssage()
        {
            OperationHolder = new TaskHolder<T>();
        }

        public ServiceAsyncMesssage(object sender)
            : base(sender)
        {
            OperationHolder = new TaskHolder<T>();
        }

        public ServiceAsyncMesssage(object sender, object target)
            : base(sender, target)
        {
            OperationHolder = new TaskHolder<T>();
        }

        public ITaskHolder<T> OperationHolder { get; protected set; }
    }
}