namespace StrongBeaver.Core.Services.Async
{
    public interface IServiceAsyncMesssage : IServiceMessage
    {
        ITaskHolder OperationHolder { get; }
    }

    public interface IServiceAsyncMesssage<T> : IServiceMessage
    {
        ITaskHolder<T> OperationHolder { get; }
    }
}