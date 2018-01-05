using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Async
{
    public class TaskHolder : ITaskHolder
    {
        public TaskHolder()
        {
            Operation = null;
        }

        public Task Operation { get; set; }

        public bool HasAssignedOperation
        {
            get { return Operation != null; }
        }

        public bool IsOperationCompleted
        {
            get { return HasAssignedOperation && Operation.IsCompleted; }
        }

        public void AssignOperation(Task operation)
        {
            Operation = operation;
        }
    }

    public class TaskHolder<T> : ITaskHolder<T>
    {
        public TaskHolder()
        {
            Operation = null;
        }

        public Task<T> Operation { get; set; }

        public bool HasAssignedOperation
        {
            get { return Operation != null; }
        }

        public bool IsOperationCompleted
        {
            get { return HasAssignedOperation && Operation.IsCompleted; }
        }

        public void AssignOperation(Task<T> operation)
        {
            Operation = operation;
        }
    }
}
