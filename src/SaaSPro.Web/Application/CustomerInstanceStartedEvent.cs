using SaaSPro.Common;

namespace SaaSPro.Web
{
    public class CustomerInstanceStartedEvent : IEvent
    {
        public string InstanceId { get; set; }
    }
}