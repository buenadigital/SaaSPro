using System.Linq;
using SaaSPro.Common.Helpers;
using SaaSPro.Domain;

namespace SaaSPro.Data.Repositories
{
    public class EFNotificationTransport : INotificationTransport
    {
        private readonly EFDbContext _dbContext;

        public EFNotificationTransport(EFDbContext dbContext)
        {
            Ensure.Argument.NotNull(dbContext, "_dbContext");
            this._dbContext = dbContext;
        }

        public void Process(Notification notification)
        {
            Ensure.Argument.NotNull(notification, "notification");

            var message = new NotificationMessage(
                notification.NotificationType,
                notification.Subject,
                notification.Body,
                notification.ReferenceId,
                notification.Sender);

            _dbContext.Set<NotificationMessage>().Add(message);

            var users = _dbContext.Set<User>().AsQueryable();

            if (!notification.SendToAllUsers)
            {
                users = users.Where(u => notification.Recipients.Contains(u.Id));
            }

            foreach (var user in users)
            {
                _dbContext.Set<UserNotification>().Add(new UserNotification(user, message));
            }
        }
    }
}
