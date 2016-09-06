namespace SaaSPro.Common
{
    /// <summary>
    /// An interface for event buses.
    /// </summary>
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent evnt) where TEvent : IEvent;
    }
}
