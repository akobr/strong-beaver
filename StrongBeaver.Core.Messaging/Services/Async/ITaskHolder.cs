using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Async
{
    public interface ITaskHolder
    {
        Task Operation { get; }

        bool HasAssignedOperation { get; }

        bool IsOperationCompleted { get; }
    }

    public interface ITaskHolder<T>
    {
        Task<T> Operation { get; }

        bool HasAssignedOperation { get; }

        bool IsOperationCompleted { get; }
    }
}