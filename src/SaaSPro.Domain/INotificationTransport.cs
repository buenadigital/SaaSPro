namespace SaaSPro.Domain
{
    /// <summary>
    /// Interface for classes that can transport/process Notifications.
    /// </summary>
    public interface INotificationTransport
    {
        void Process(Notification notification);
    }
}
