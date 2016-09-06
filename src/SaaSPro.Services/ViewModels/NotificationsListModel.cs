using System;
using SaaSPro.Common;
using SaaSPro.Domain;

namespace SaaSPro.Services.ViewModels
{
    public class NotificationsListModel
    {
        public IPagedList<Notification> Notifications { get; set; }
        
        public class Notification
        {
            public Guid Id { get; set; }
            public NotificationType NotificationType { get; set; }
            public string Subject { get; set; }
            public DateTime CreatedOn { get; set; }
            public string Link { get; set; }

            public Notification()
            {
                Link = "#";
            }
        }
    }
}