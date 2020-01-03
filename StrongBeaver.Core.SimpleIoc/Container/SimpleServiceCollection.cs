using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace StrongBeaver.Core.Container
{
    public class SimpleServiceCollection : List<ServiceDescriptor>, IServiceCollection
    {
        // no member
    }
}
