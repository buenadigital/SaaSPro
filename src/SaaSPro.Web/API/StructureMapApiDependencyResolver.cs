using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace SaaSPro.Web
{
    public class StructureMapDependencyScope : IDependencyScope, IServiceProvider
    {
        public IContainer Container { get; private set; }

        public StructureMapDependencyScope(IContainer container)
        {
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsAbstract || serviceType.IsInterface
                     ? Container.TryGetInstance(serviceType)
                     : Container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }
        }
    }

    public class StructureMapApiDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        private readonly IContainer container;

        public StructureMapApiDependencyResolver(IContainer container)
            : base(container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            var childContainer = this.container.GetNestedContainer();
            return new StructureMapDependencyScope(childContainer);
        }
    }
}