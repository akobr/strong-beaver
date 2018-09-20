using System;
using StrongBeaver.Core.Services.Logging;

namespace StrongBeaver.Core.Services.Reflection
{
    public class InstanceCreationService : BaseDisposableService, IInstanceCreationService
    {
        private readonly ILogService logging;
        private Func<Type, object> complexFactory;

        public InstanceCreationService(ILogService logging)
        {
            this.logging = logging;
            complexFactory = Activator.CreateInstance;
        }

        public TType CreateInstance<TType>()
        {
            try
            {
                Type type = typeof(TType);

                if (HasParameterlessConstructor(type))
                {
                    return Activator.CreateInstance<TType>();
                }

                return (TType)complexFactory(type);
            }
            catch (Exception ex)
            {
                logging.Error(
                    "Error occured during creating new instance by reflection.",
                    ex, typeof(TType));
                throw;
            }
        }

        public object CreateInstance(Type instanceType)
        {
            try
            {
                if (HasParameterlessConstructor(instanceType))
                {
                    return Activator.CreateInstance(instanceType);
                }

                return complexFactory(instanceType);
            }
            catch (Exception ex)
            {
                logging.Error(
                    "Error occured during creating new instance by reflection.",
                    ex, instanceType);
                throw;
            }
        }

        public TType CreateInstance<TType>(object[] arguments)
        {
            return (TType)CreateInstance(typeof(TType), arguments);
        }

        public object CreateInstance(Type instanceType, object[] arguments)
        {
            try
            {
                return Activator.CreateInstance(instanceType, arguments);
            }
            catch (Exception ex)
            {
                logging.Error(
                    "Error occured during creating new instance by reflection.",
                    ex, instanceType);
                throw;
            }
        }

        public void SetComplexCreationFactory(Func<Type, object> factory)
        {
            if (complexFactory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            complexFactory = factory;
        }

        private static bool HasParameterlessConstructor(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null;
        }
    }
}