using System;
using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public class SimpleServiceProviderFactory : IServiceProviderFactory<SimpleContainerBuilder>
    {
        public SimpleContainerBuilder CreateBuilder()
        {
            return new SimpleContainerBuilder();
        }

        public SimpleContainerBuilder CreateBuilder(IServiceCollection services)
        {
            return new SimpleContainerBuilder(services);
        }

        public IServiceProvider CreateServiceProvider(SimpleContainerBuilder containerBuilder)
        {
            return containerBuilder.Container;
        }
    }
}
