using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.SubscriptionsService;
using SaaSPro.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace SaaSPro.Web.Controllers
{
    public class NotificationsController : SaaSProControllerBase
    {
        private readonly IUserNotificationService _userNotificationService;

        public NotificationsController(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
        }

        [HttpGet]
        public ActionResult Index(GetCurrentUserNotificationsRequest request)
        {
            request.UserId = User.Id;
            NotificationsListModel model = _userNotificationService.GetCurrentUserNotifications(request);

            return ViewOrPartial(model, partialViewName: "_Index");
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            bool result=_userNotificationService.Delete(id,User.Id);

            return Json(result);
        }

        
    }
}