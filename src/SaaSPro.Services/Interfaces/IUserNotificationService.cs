using System;
using SaaSPro.Services.Messaging.SubscriptionsService;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Interfaces
{
    public interface IUserNotificationService
    {
        NotificationsListModel GetCurrentUserNotifications(GetCurrentUserNotificationsRequest request);
        bool Delete(Guid id, Guid userId);
    }
}