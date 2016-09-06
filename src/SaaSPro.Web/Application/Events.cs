using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using System;

namespace SaaSPro.Web
{
    public static class Events
    {
        static Lazy<IEventBus> _bus;
       
        public static void Raise<TEvent>(TEvent evnt) where TEvent : IEvent
        {
            Ensure.Argument.NotNull(evnt, "evnt");

            _bus?.Value.Publish(evnt);
        }

        public static void RegisterEventBus(Func<IEventBus> eventBusFactory)
        {
            Ensure.Argument.NotNull(eventBusFactory, "eventBusFactory");
            _bus = new Lazy<IEventBus>(eventBusFactory);
        }
    }
}