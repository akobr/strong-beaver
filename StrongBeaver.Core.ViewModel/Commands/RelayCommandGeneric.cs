using System;
using System.Windows.Input;
using StrongBeaver.Core.Delegates;

namespace StrongBeaver.Core.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private readonly WeakAction<T> execute;
        private readonly WeakFunc<T, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<T> execute, bool keepTargetAlive = false)
            : this(execute, null, keepTargetAlive)
        {
            // no operation
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute, bool keepTargetAlive = false)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            this.execute = new WeakAction<T>(execute, keepTargetAlive);

            if (canExecute != null)
            {
                this.canExecute = new WeakFunc<T,bool>(canExecute, keepTargetAlive);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            if (canExecute.IsStatic || canExecute.IsAlive)
            {
                if (parameter == null
                    && typeof(T).IsValueType)
                {
                    return canExecute.Execute(default(T));
                }

                if (parameter == null || parameter is T)
                {
                    return (canExecute.Execute((T)parameter));
                }
            }

            return false;
        }

        public virtual void Execute(object parameter)
        {
            var val = parameter;

            if (parameter != null
                && parameter.GetType() != typeof(T))
            {
                if (parameter is IConvertible)
                {
                    val = Convert.ChangeType(parameter, typeof (T), null);
                }
            }

            if (CanExecute(val)
                && execute != null
                && (execute.IsStatic || execute.IsAlive))
            {
                if (val == null)
                {
                    execute.Execute(default(T));
                }
                else
                {
                    execute.Execute((T)val);
                }
            }
        }
    }
}