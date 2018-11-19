using System;
using System.Reflection;

namespace StrongBeaver.Core.Delegates
{
    public class WeakAction
    {
        private Action staticAction;

        protected WeakAction()
        {
            // no operation
        }

        public WeakAction(Action action, bool keepTargetAlive = false)
            : this(action?.Target, action, keepTargetAlive)
        {
            // no operation
        }

        public WeakAction(object target, Action action, bool keepTargetAlive = false)
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

        public object Target => Reference?.Target;
        public bool IsStatic => staticAction != null;

        public virtual string MethodName
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

        public virtual bool IsAlive
        {
            get
            {
                if (staticAction == null
                    && Reference == null
                    && LiveReference == null)
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

                // Non static action
                if (LiveReference != null)
                {
                    return true;
                }

                if (Reference != null)
                {
                    return Reference.IsAlive;
                }

                return false;
            }
        }

        protected MethodInfo Method { get; set; }

        protected WeakReference ActionReference { get; set; }

        protected object LiveReference { get; set; }

        protected WeakReference Reference { get; set; }

        protected object ActionTarget
        {
            get
            {
                if (LiveReference != null)
                {
                    return LiveReference;
                }

                return ActionReference?.Target;
            }
        }

        public void Execute()
        {
            if (staticAction != null)
            {
                staticAction();
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
                    Method.Invoke(actionTarget, null);
                }

            }
        }

        public void MarkForDeletion()
        {
            Reference = null;
            ActionReference = null;
            LiveReference = null;
            Method = null;
            staticAction = null;
        }
    }
}