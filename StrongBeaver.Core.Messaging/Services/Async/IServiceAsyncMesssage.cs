namespace StrongBeaver.Core.Services.Async
{
    public interface IServiceAsyncMesssage : IServiceMessage
    {
        ITaskHolder OperationHolder { get; }
    }

    public interface IServiceAsyncMesssage<TResult> : IServiceMessage
    {
        ITaskHolder<TResult> OperationHolder { get; }
    }

    public interface IServiceAsyncMesssage<TResult, in TService> : IServiceMessage<TService>
        where TService : IService
    {
        ITaskHolder<TResult> OperationHolder { get; }
    }
}