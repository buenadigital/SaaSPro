using System;
using System.IO;
using System.Web.Mvc;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Services.Interfaces;
using Stripe;
using System.Net;

namespace SaaSPro.Web.Management.Controllers
{
    [AllowAnonymous]
    public class StripeWebhookController : Controller
    {
 
        private readonly IStripeWebhookService _stripeWebhookService;

        public StripeWebhookController(IStripeWebhookService stripeWebhookService)
        {
            _stripeWebhookService = stripeWebhookService;
        }

        [HttpPost]
        public ActionResult Index()
        {
            LoggingFactory.GetLogger().Log("StripeWebhookController Called", EventLogSeverity.Debug);
            var req = Request.InputStream;
            req.Seek(0, SeekOrigin.Begin);

            var json = new StreamReader(req).ReadToEnd();
            LoggingFactory.GetLogger().Log("StripeWebhookController Json: " + json, EventLogSeverity.Debug);
            try
            {
                var stripeEvent = StripeEventUtility.ParseEvent(json);

                if (stripeEvent == null || (stripeEvent.Data == null) || (stripeEvent.Data.Object == null))
                {
                    LoggingFactory.GetLogger().Log("StripeWebhookController Incoming event empty", EventLogSeverity.Debug);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Incoming event empty");
                }
                string customerID = string.Empty;

                if (stripeEvent.Data.Object.customer!=null)
                {
                    customerID = stripeEvent.Data.Object.customer.ToString();
                }
                else if (stripeEvent.Data.Object.id != null)
                {
                    customerID = stripeEvent.Data.Object.id.ToString();
                }

                if (!_stripeWebhookService.ValidateCustomer(customerID))
                {
                    LoggingFactory.GetLogger().Log("StripeWebhookController Customer Not Valid " + customerID.ToString(), EventLogSeverity.Debug);
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Customer Not Valid");
                }
                _stripeWebhookService.ProcessEvent(customerID, stripeEvent);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                LoggingFactory.GetLogger().LogError(ex);

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unable to parse incoming event");
            }
        }
    }
}