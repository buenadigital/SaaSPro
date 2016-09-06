using SaaSPro.Domain;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.Areas.Admin.ViewModels
{
    public class NotificationsSendModel
    {
        [Required]
        public NotificationType NotificationType { get; set; }
        
        [Required]
        public string Message { get; set; }
    }
}