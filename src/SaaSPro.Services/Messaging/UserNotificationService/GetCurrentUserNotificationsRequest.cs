using System;

namespace SaaSPro.Services.Messaging.SubscriptionsService
{
    public class GetCurrentUserNotificationsRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Guid UserId { get; set; }

        public GetCurrentUserNotificationsRequest()
        {
            Page = 1;
            PageSize = 10;
        }
    }
}
