using SaaSPro.Common;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;
using System.Web.Mvc;
using SaaSPro.Data.Repositories;
using System.Configuration;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Mapping;
using SaaSPro.Infrastructure.Email;
using System.Net.Mail;
using System;
using SaaSPro.Infrastructure.Logging;

namespace SaaSPro.Web.Front.Controllers
{
    public class ContactUsController : Controller
    {
        //IEmailService
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;

        public ContactUsController(IEmailTemplatesRepository emailTemplatesRepository)
        {
            _emailTemplatesRepository = emailTemplatesRepository;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string plan, ContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // send email
                    EmailTemplate emailTemplate = _emailTemplatesRepository.GetTemplateByName(
                            SaaSPro.Common.Helpers.Utilities.ToDescriptionString(EmailTemplateCode.ContactRequest));

                    model.MailTo = ConfigurationManager.AppSettings["MailTo"];

                    emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, model);
                    MailMessage mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);
                    bool emailResult = EmailServiceFactory.GetEmailService().SendMail(mailMessage);

                    if (emailResult)
                    {
                        return RedirectToAction("thank-you");
                    }

                    ModelState.AddModelError("", "There was a problem ");

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

        [ActionName("Thank-You")]
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}