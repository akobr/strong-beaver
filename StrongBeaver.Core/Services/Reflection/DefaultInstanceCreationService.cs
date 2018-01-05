using System;

namespace StrongBeaver.Core.Services.Reflection
{
    public class DefaultInstanceCreationService : BaseService, IInstanceCreationService
    {
        public TType CreateInstance<TType>()
        {
            try
            {
                return Activator.CreateInstance<TType>();
            }
            catch (Exception ex)
            {
                Provider.LogErrorMessage(
                    "Error occured during creating new instance by reflection.",
                    ex, typeof(TType));
                throw;
            }
        }

        public object CreateInstance(Type instanceType)
        {
            try
            {
                return Activator.CreateInstance(instanceType);
            }
            catch (Exception ex)
            {
                Provider.LogErrorMessage(
                    "Error occured during creating new instance by reflection.",
                    ex, instanceType);
                throw;
            }
        }

        public object CreateInstance(Type instanceType, object[] arguments)
        {
            try
            {
                return Activator.CreateInstance(instanceType, arguments);
            }
            catch (Exception ex)
            {
                Provider.LogErrorMessage(
                    "Error occured during creating new instance by reflection.",
                    ex, instanceType);
                throw;
            }
        }
    }
}