using System;

namespace SaaSPro.Common
{
    /// <summary>
    /// A default event bus that publishes events to handlers created by a <see cref="IEventHandlerFactory"/>.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly IEventHandlerFactory eventHandlerFactory;

        public EventBus(IEventHandlerFactory eventHandlerFactory)
        {
            if (eventHandlerFactory == null)
            {
                throw new ArgumentNullException(nameof(eventHandlerFactory));
            }

            this.eventHandlerFactory = eventHandlerFactory;
        }
        
        public void Publish<TEvent>(TEvent evnt) where TEvent : IEvent
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }

            var handlers = eventHandlerFactory.CreateHandlers<TEvent>();
            foreach (var handler in handlers)
            {
                Handle(evnt, handler);
            }
        }

        private void Handle<TEvent>(TEvent evnt, IHandleEvent<TEvent> handler) where TEvent : IEvent
        {
            try
            {
                handler.Handle(evnt);
            }
            catch
            {
            }
            finally
            {
                var disposable = handler as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
