using System;
using System.Windows.Input;
using StrongBeaver.Core.Delegates;

namespace StrongBeaver.Core.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly WeakAction execute;
        private readonly WeakFunc<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute, bool keepTargetAlive = false)
            : this(execute, null, keepTargetAlive)
        {
            // no operation
        }

        public RelayCommand(Action execute, Func<bool> canExecute, bool keepTargetAlive = false)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = new WeakAction(execute, keepTargetAlive);

            if (canExecute != null)
            {
                this.canExecute = new WeakFunc<bool>(canExecute, keepTargetAlive);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null
                || (canExecute.IsStatic || canExecute.IsAlive)
                    && canExecute.Execute();
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter)
                && execute != null
                && (execute.IsStatic || execute.IsAlive))
            {
                execute.Execute();
            }
        }
    }
}