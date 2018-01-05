using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Async
{
    public interface ITaskHolder
    {
        Task Operation { get; }

        bool HasAssignedOperation { get; }

        bool IsOperationCompleted { get; }

        void AssignOperation(Task operation);
    }

    public interface ITaskHolder<T>
    {
        Task<T> Operation { get; }

        bool HasAssignedOperation { get; }

        bool IsOperationCompleted { get; }

        void AssignOperation(Task<T> operation);
    }
}