using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using StructureMap;
using System.Collections.Generic;

namespace SaaSPro.Web
{
    public class StructureMapEventHandlerFactory : IEventHandlerFactory
    {
        private readonly IContainer _container;

        public StructureMapEventHandlerFactory(IContainer container)
        {
            Ensure.Argument.NotNull(container, nameof(container));
            _container = container;
        }
        
        public IEnumerable<IHandleEvent<TEvent>> CreateHandlers<TEvent>() where TEvent : IEvent
        {
            return _container.GetAllInstances<IHandleEvent<TEvent>>();
        }
    }
}