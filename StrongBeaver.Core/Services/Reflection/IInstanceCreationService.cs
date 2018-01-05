using System;

namespace StrongBeaver.Core.Services.Reflection
{
    public interface IInstanceCreationService : IService
    {
        TType CreateInstance<TType>();

        object CreateInstance(Type instanceType);

        object CreateInstance(Type instanceType, object[] arguments);
    }
}