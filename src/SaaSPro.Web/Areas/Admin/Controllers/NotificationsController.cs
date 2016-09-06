using SaaSPro.Domain;
using SaaSPro.Web.Areas.Admin.ViewModels;
using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class NotificationsController : AdminControllerBase
    {
        INotificationCenter notificationCenter;

        public NotificationsController(INotificationCenter notificationCenter)
        {
            this.notificationCenter = notificationCenter;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Send(NotificationsSendModel model)
        {
            notificationCenter.SendToAllUsers(model.NotificationType, model.Message);
        }
    }
}
