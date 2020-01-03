using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Async
{
    public class TaskHolder : ITaskHolder
    {
        public TaskHolder(Task operation)
        {
            Operation = operation;
        }

        public TaskHolder()
            : this(null)
        {
            // no operation
        }

        public Task Operation { get; set; }

        public bool HasAssignedOperation => Operation != null;

        public bool IsOperationCompleted => HasAssignedOperation && Operation.IsCompleted;
    }

    public class TaskHolder<T> : ITaskHolder<T>
    {
        public TaskHolder(Task<T> operation)
        {
            Operation = operation;
        }

        public TaskHolder()
            : this(null)
        {
            // no operation
        }

        public Task<T> Operation { get; set; }

        public bool HasAssignedOperation => Operation != null;

        public bool IsOperationCompleted => HasAssignedOperation && Operation.IsCompleted;
    }
}
