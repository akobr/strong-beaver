using System;

namespace StrongBeaver.Core.Delegates
{
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        private Action<T> staticAction;

        public WeakAction(Action<T> action, bool keepTargetAlive = false)
            : this(action?.Target, action, keepTargetAlive)
        {
        }

        public WeakAction(object target, Action<T> action, bool keepTargetAlive = false)
        {
            if (action.Method.IsStatic)
            {
                staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    Reference = new WeakReference(target);
                }

                return;
            }

            Method = action.Method;
            ActionReference = new WeakReference(action.Target);
            LiveReference = keepTargetAlive ? action.Target : null;
            Reference = new WeakReference(target);

#if DEBUG
            if (ActionReference != null
                && ActionReference.Target != null
                && !keepTargetAlive)
            {
                var type = ActionReference.Target.GetType();

                if (type.Name.StartsWith("<>")
                    && type.Name.Contains("DisplayClass"))
                {
                    System.Diagnostics.Debug.WriteLine(
                        "You are attempting to register a lambda with a closure without using keepTargetAlive. Are you sure?");
                }
            }
#endif
        }

        public override string MethodName
        {
            get
            {
                if (staticAction != null)
                {
                    return staticAction.Method.Name;
                }

                return Method.Name;
            }
        }

        public override bool IsAlive
        {
            get
            {
                if (staticAction == null
                    && Reference == null)
                {
                    return false;
                }

                if (staticAction != null)
                {
                    if (Reference != null)
                    {
                        return Reference.IsAlive;
                    }

                    return true;
                }

                return Reference.IsAlive;
            }
        }

        public new void Execute()
        {
            Execute(default(T));
        }

        public void Execute(T parameter)
        {
            if (staticAction != null)
            {
                staticAction(parameter);
                return;
            }

            var actionTarget = ActionTarget;

            if (IsAlive)
            {
                if (Method != null
                    && (LiveReference != null
                        || ActionReference != null)
                    && actionTarget != null)
                {
                    Method.Invoke(
                        actionTarget,
                        new object[]
                        {
                            parameter
                        });
                }
            }
        }

        public void ExecuteWithObject(object parameter)
        {
            var parameterCasted = (T)parameter;
            Execute(parameterCasted);
        }

        public new void MarkForDeletion()
        {
            staticAction = null;
            base.MarkForDeletion();
        }
    }
}