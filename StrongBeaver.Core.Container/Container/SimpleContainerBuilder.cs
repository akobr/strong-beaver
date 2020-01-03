using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public class SimpleContainerBuilder : ISimpleContainerBuilder
    {
        public SimpleContainerBuilder()
        {
            Container = new SimpleContainer();
        }

        public SimpleContainerBuilder(IServiceCollection services)
        {
            Container = new SimpleContainer(services);
        }

        public bool Writable { get; set; }

        public SimpleContainer Container { get; }

        public IServiceCollection CreateCollection()
        {
            return new SimpleServiceCollection();
        }

        public ISimpleContainer CreateContainer()
        {
            return Writable ? Container : CreateReadonlyContainer();
        }

        public ISimpleContainer CreateReadonlyContainer()
        {
            return new SimpleReadonlyContainer(Container);
        }
    }
}
