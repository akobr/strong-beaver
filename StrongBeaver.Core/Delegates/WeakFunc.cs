using System;
using System.Reflection;

namespace StrongBeaver.Core.Delegates
{
    public class WeakFunc<TResult>
    {
        private Func<TResult> staticFunc;

        protected WeakFunc()
        {
            // no operation
        }

        public WeakFunc(Func<TResult> func, bool keepTargetAlive = false)
            : this(func?.Target, func, keepTargetAlive)
        {
            // no operation
        }

        public WeakFunc(object target, Func<TResult> func, bool keepTargetAlive = false)
        {
            if (func.Method.IsStatic)
            {
                staticFunc = func;

                if (target != null)
                {
                    // Keep a reference to the target to control the
                    // WeakAction's lifetime.
                    Reference = new WeakReference(target);
                }

                return;
            }

            Method = func.Method;
            FuncReference = new WeakReference(func.Target);
            LiveReference = keepTargetAlive ? func.Target : null;
            Reference = new WeakReference(target);

#if DEBUG
            if (FuncReference != null
                && FuncReference.Target != null
                && !keepTargetAlive)
            {
                var type = FuncReference.Target.GetType();

                if (type.Name.StartsWith("<>")
                    && type.Name.Contains("DisplayClass"))
                {
                    System.Diagnostics.Debug.WriteLine(
                        "You are attempting to register a lambda with a closure without using keepTargetAlive. Are you sure?");
                }
            }
#endif
        }

        public bool IsStatic => staticFunc != null;

        public virtual string MethodName
        {
            get
            {
                if (staticFunc != null)
                {
                    return staticFunc.Method.Name;
                }

                return Method.Name;
            }
        }

        public virtual bool IsAlive
        {
            get
            {
                if (staticFunc == null
                    && Reference == null
                    && LiveReference == null)
                {
                    return false;
                }

                if (staticFunc != null)
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

        public object Target => Reference?.Target;

        protected MethodInfo Method { get; set; }

        protected WeakReference FuncReference { get; set; }

        protected object LiveReference { get; set; }

        protected WeakReference Reference { get; set; }


        protected object FuncTarget
        {
            get
            {
                if (LiveReference != null)
                {
                    return LiveReference;
                }

                return FuncReference?.Target;
            }
        }

        public TResult Execute()
        {
            if (staticFunc != null)
            {
                return staticFunc();
            }

            var funcTarget = FuncTarget;

            if (IsAlive)
            {
                if (Method != null
                    && (LiveReference != null
                        || FuncReference != null)
                    && funcTarget != null)
                {
                    return (TResult)Method.Invoke(funcTarget, null);
                }
            }

            return default(TResult);
        }

        public void MarkForDeletion()
        {
            Reference = null;
            FuncReference = null;
            LiveReference = null;
            Method = null;
            staticFunc = null;
        }
    }
}