using SaaSPro.Common.Helpers;
using SaaSPro.Domain;
using SaaSPro.Web.Hubs;
using Microsoft.AspNet.SignalR;

namespace SaaSPro.Web
{
    public class SignalRNotificationTransport : INotificationTransport
    {
        private readonly CustomerInstance _customer;
        private readonly IHubContext _hub;

        public SignalRNotificationTransport(CustomerInstance customer)
        {
            _customer = customer;
            _hub = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
        }

        public void Process(Notification notification)
        {
            Ensure.Argument.NotNull(notification, nameof(notification));
            
            var message = new {
                notification.Subject,
                NotificationType = notification.NotificationType.ToString().SeparatePascalCase()
            };

            if (notification.SendToAllUsers)
            {
                // sent to all users for current Customer
                _hub.Clients.Group(_customer.CustomerId.ToString()).notify(message);
            }
            else 
            {
                // sent to user specific group for each recipient
                foreach (var recipient in notification.Recipients)
                {
                    _hub.Clients.Group(recipient.ToString()).notify(message);
                }
            }
        }
    }
}