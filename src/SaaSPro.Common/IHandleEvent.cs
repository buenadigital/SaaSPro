namespace SaaSPro.Common
{
    /// <summary>
    /// An interface for event handlers.
    /// </summary>
    /// <typeparam name="TEvent">The type of event to handle</typeparam>
    public interface IHandleEvent<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent evnt);
    }
}
