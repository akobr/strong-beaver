using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public class SimpleContainerBuilder
    {
        public SimpleContainerBuilder()
        {
            Container = new SimpleContainer();
        }

        public SimpleContainerBuilder(IServiceCollection services)
        {
            Container = new SimpleContainer(services);
        }

        public SimpleContainer Container { get; }
    }
}
