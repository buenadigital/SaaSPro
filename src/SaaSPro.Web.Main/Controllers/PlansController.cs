using System;
using System.Linq;
using System.Web.Mvc;
using SaaSPro.Common;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Services.Messaging.PlanService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Services.Interfaces;
using System.Configuration;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Data.Repositories;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Mapping;

namespace SaaSPro.Web.Front.Controllers
{
    public class PlansController : Controller
    {
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService, IEmailTemplatesRepository emailTemplatesRepository)
        {
            _emailTemplatesRepository = emailTemplatesRepository;
            _planService = planService;
        }

        [HttpGet]
        public ActionResult Pricing()
        {
            PricingModel pricingModel = _planService.GetPricing();
            return View(pricingModel);
        }

        [HttpGet]
        public ActionResult SignUp(string plan)
        {
            if (!_planService.PlanCodeExist(plan))
            {
                return RedirectToAction("Pricing", "Plans");
            }

            SignUpModel model = new SignUpModel();
            model.PlanName = char.ToUpper(plan[0]) + ((plan.Length > 1) ? plan.Substring(1).ToLower() : string.Empty);

            return View(model);
        }

        [HttpPost]
        public ActionResult SignUp(string plan, SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PlanSignUpRequest request = new PlanSignUpRequest
                    {
                        PlanName = Request.QueryString["plan"],
                        SignUpModel = model
                    };
                    PlanSignUpResponse response = _planService.PlanSignUp(request);

                    if (response.HasError && response.ErrorCode == ErrorCode.PlanNotFound)
                    {
                        return RedirectToAction("Pricing", "Plans");
                    }

                    if (response.HasError && response.ErrorCode == ErrorCode.DomainAlreadyExists)
                    {
                        ModelState.AddModelError("Domain", "Domain already exists");
                    }

                    var domainURL = "https://" + $"{model.Domain}{ConfigurationManager.AppSettings["HostnameBase"]}";
                    var emailTemplate = _emailTemplatesRepository.GetTemplateByName(Common.Helpers.Utilities.ToDescriptionString(EmailTemplateCode.SignUpGreeting));

                    model.PlanName = plan;
                    emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, model);
                    var mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);

                    EmailServiceFactory.GetEmailService().SendMail(mailMessage);

                    TempData["Domain"] = domainURL;
                    return RedirectToAction("thank-you");
                }
                catch (Exception ex)
                {
                    var errMsg = ex.Message;
                    if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                        errMsg += ", " + ex.InnerException.Message;

                    LoggingFactory.GetLogger().Log(errMsg, EventLogSeverity.Error);
                    ModelState.AddModelError("", errMsg);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public string CheckUserName(string input)
        {
            return _planService.IsHostNameAvailable(input) ? "Available" : "This domain is not available. Please try a different one";
        }

        [HttpGet]
        [ActionName("thank-you")]
        public ActionResult ThankYou()
        {
            return View("ThankYou", model: TempData["Domain"]);
        }

        [HttpGet]
        [ActionName("get-regions")]
        public ActionResult GetRegions(string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            var country = Country.FindByIsoCode(countryCode);
            if (country != null)
            {
                return Json(new { success = true, regions = country.Regions.Select(r => new { name = r.Name, code = r.Abbreviation }) }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
