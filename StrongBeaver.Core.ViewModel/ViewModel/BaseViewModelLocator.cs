using System.Collections.Generic;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Core.ViewModel
{
    public abstract class BaseViewModelLocator : IBaseViewModelLocator
    {
        private readonly IList<IViewModel> models;
        private ILogService logging;

        protected BaseViewModelLocator()
        {
            models = new List<IViewModel>();
        }

        protected BaseViewModelLocator(ILogService logging, IEnvironmentViewModel environment)
            : this()
        {
            Startup(logging, environment);
        }

        public IEnvironmentViewModel Environment { get; private set; }

        public bool IsDebugMode => IsDebugModeStatic;

        public static bool IsDebugModeStatic
#if DEBUG
            => true;
#else
            => false;
#endif

        public void Initialise()
        {
            OnInitialise();
            logging.Debug($"The ViewModel locator '{GetType().Name}' has been initialised.");
        }

        public void Cleanup()
        {
            OnCleanup();
            logging.Debug($"The ViewModel locator '{GetType().Name}' has been cleanup.");
        }

        protected void Startup(ILogService logService, IEnvironmentViewModel environmentModel)
        {
            logging = logService;

            Environment = environmentModel;
            RegisterViewModel(environmentModel);
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