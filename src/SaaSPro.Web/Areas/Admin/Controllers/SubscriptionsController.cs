using System;
using System.Web.Mvc;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.SubscriptionsService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Common;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class SubscriptionsController : AdminControllerBase
    {
        private readonly ISubscriptionsService _subscriptionsService;
        private readonly IPlanService _planService;

        public SubscriptionsController(ISubscriptionsService subscriptionsService,IPlanService planService)
        {
            _subscriptionsService = subscriptionsService;
            _planService = planService;
        }

        public ActionResult Index()
        {
            GetSubscriptionsResponse response = _subscriptionsService.List(Customer.CustomerId);

            if (!response.HasPlan)
            {
                return View("ChangeSubscription", response.ChangeSubscriptionModel);
            }

            return View(response.SubscriptionModel);
        }

        [ActionName("cancel-subscription")]
        public ActionResult CancelSubscription()
        {
            CancelSubscriptionResponse response=_subscriptionsService.CancelSubscription(Customer.CustomerId);

            if (response.IsStatusActive)
            {
                return RedirectToAction("index", "subscriptions")
                    .AndAlert(AlertType.Danger, "Error.", "The subscription wasn't cancelled.");
            }

            return RedirectToAction("index").AndAlert(AlertType.Warning, "Cancelled.", "The subscription was cancelled successfully.");
        }

        [HttpGet]
        [ActionName("change-subscription")]
        public ActionResult ChangeSubscription()
        {
            ChangeSubscriptionModel model = new ChangeSubscriptionModel
            {
                Plans = _planService.List()
            };

            return View("ChangeSubscription", model);
        }

        [HttpPost]
        [ActionName("change-subscription")]
        public ActionResult ChangeSubscription(ChangeSubscriptionModel model)
        {

            if (ModelState.IsValid)
            {
                ChangeSubscriptionRequest request = new ChangeSubscriptionRequest
                {
                    CustomerId = Customer.CustomerId,
                    ChangeSubscriptionModel = model,
                    PlanId = model.PlanId
                };
                ChangeSubscriptionResponse response = _subscriptionsService.ChangeSubscription(request);

                if (response.IsStatusActive)
                {
                    return RedirectToAction("index", "subscriptions").AndAlert(AlertType.Danger, "Error.",
                                                                               "Previous subscription wasn't cancelled.");
                }

                if (response.ErrorCode == ErrorCode.IncorrectPlanIdentifier)
                {
                    throw new Exception(response.ErrorCode.ToString());
                }

                if (response.Result != "active")
                {
                    return RedirectToAction("index", "subscriptions")
                        .AndAlert(AlertType.Danger, "Error.", response.Message);
                }

                return RedirectToAction("index").AndAlert(AlertType.Success, "Changed.",
                                                          "The subscription was changed successfully.");
            }

            model.Plans = _planService.List();
            return View("ChangeSubscription", model);
        }
    }
}
