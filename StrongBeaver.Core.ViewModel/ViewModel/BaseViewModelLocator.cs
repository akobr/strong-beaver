using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace StrongBeaver.Core.ViewModel
{
    public abstract class BaseViewModelLocator : IBaseViewModelLocator
    {
        private readonly IList<IViewModel> models;

        protected BaseViewModelLocator(IEnvironmentViewModel environmentViewModel)
        {
            Environment = environmentViewModel;
            RegisterViewModel(environmentViewModel);

            models = new List<IViewModel>();
        }

        public IEnvironmentViewModel Environment { get; }

        public bool IsDebugMode => IsDebugModeStatic;

        public bool IsInDesignModel => IsInDesignModeStatic;

        public static bool IsDebugModeStatic
#if DEBUG
            => true;
#else
            => false;
#endif

        public static bool IsInDesignModeStatic => ViewModelBase.IsInDesignModeStatic;

        public void Initialise()
        {
            OnInitialise();
            Provider.LogDebugMessage($"The ViewModel locator '{GetType().Name}' has been initialised.");
        }

        public void Cleanup()
        {
            OnCleanup();
            Provider.LogDebugMessage($"The ViewModel locator '{GetType().Name}' has been cleanup.");

        }

        protected virtual void OnInitialise()
        {
            foreach (IViewModel model in models)
            {
                model.Initialise();
            }
        }

        protected virtual void OnCleanup()
        {
            foreach (IViewModel model in models)
            {
                model.Cleanup();
            }
        }

        protected void RegisterViewModel(IViewModel model)
        {
            if (model == null)
            {
                return;
            }

            models.Add(model);
        }
    }
}
