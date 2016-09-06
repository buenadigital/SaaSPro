using System.Collections.Generic;

namespace SaaSPro.Common
{
    /// <summary>
    /// An interface for classes that create event handlers.
    /// </summary>
    public interface IEventHandlerFactory
    {
        IEnumerable<IHandleEvent<TEvent>> CreateHandlers<TEvent>() where TEvent : IEvent;
    }
}
