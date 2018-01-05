namespace StrongBeaver.Core.Services.Progress
{
    public interface IProgressService : IService
    {
        bool IsAnyOperationInProgress { get; }

        int OperationInProgressCount { get; }

        void ShowOperationProgress(string operationKey, string message);

        void ShowOperationProgress(string operationKey, string message, int percentages);

        void FinishOperationProgress(string operationKey);

        bool IsOperationInProgress(string operationKey);
    }
}
