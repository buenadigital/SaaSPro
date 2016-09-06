using System;
using System.Collections.Generic;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    public class NotificationCenter : INotificationCenter
    {
        private readonly IEnumerable<INotificationTransport> transports;

        public NotificationCenter(IEnumerable<INotificationTransport> transports)
        {
            Ensure.Argument.NotNull(transports, "transports");
            this.transports = transports;
        }
        
        public void SendToAllUsers(
            NotificationType notificationType, 
            string subject, 
            string body = null, 
            Guid? referenceId = null, 
            User sender = null)
        {
            var notification = new Notification(
                notificationType,
                subject,
                null /* recipients */,
                body,
                referenceId,
                sender,
                sendToAllUsers: true);

            Send(notification);
        }

        private void Send(Notification notification)
        {
            foreach (var transport in transports)
            {
                transport.Process(notification);
            }
        }
    }
}
