using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public class SimpleServiceProviderFactory : IServiceProviderFactory<ISimpleContainerBuilder>
    {
        public IServiceCollection CreateCollection()
        {
            return new SimpleServiceCollection();
        }

        public ISimpleContainerBuilder CreateBuilder()
        {
            return new SimpleContainerBuilder();
        }

        public ISimpleContainerBuilder CreateBuilder(IServiceCollection services)
        {
            return new SimpleContainerBuilder(services);
        }

        public IServiceProvider CreateServiceProvider(ISimpleContainerBuilder containerBuilder)
        {
            return containerBuilder.CreateContainer();
        }
    }
}
