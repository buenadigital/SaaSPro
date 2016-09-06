using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using System;

namespace SaaSPro.Domain
{
    public static class DomainEvents
    {
        private static Lazy<IEventBus> bus;

        public static void RegisterEventBus(Func<IEventBus> eventBusFactory)
        {
            Ensure.Argument.NotNull(eventBusFactory, nameof(eventBusFactory));
            bus = new Lazy<IEventBus>(eventBusFactory);
        }
    }
}
