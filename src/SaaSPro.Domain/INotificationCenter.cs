using System;

namespace SaaSPro.Domain
{
    public interface INotificationCenter
    {
        void SendToAllUsers(
            NotificationType notificationType,
            string subject,
            string body = null,
            Guid? referenceId = null,
            User sender = null);
    }
}
