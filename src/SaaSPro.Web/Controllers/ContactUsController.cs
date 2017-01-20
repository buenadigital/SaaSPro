using SaaSPro.Common;
using SaaSPro.Common.Helpers;
using SaaSPro.Data.Repositories;
using SaaSPro.Infrastructure.Email;
using SaaSPro.Infrastructure.Logging;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Mapping;
using SaaSPro.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaaSPro.Web.Controllers
{
    public class ContactUsController : Controller
    {
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
        public ActionResult SendRequest(ContactUsSupportModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MailTo = ConfigurationManager.AppSettings["SendEmailTo"];

                    var emailTemplate = _emailTemplatesRepository.GetTemplateByName(Utilities.ToDescriptionString(EmailTemplateCode.SupportContactUs));
                    emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, model);

                    var mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);

                    var sent = EmailServiceFactory.GetEmailService().SendMail(mailMessage);

                    if (sent == true)
                        return Json(new { success = true });
                    else
                        return Json(new { success = false });
                }
                catch (Exception ex)
                {
                    var errMsg = ex.Message;
                    if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message))
                        errMsg += ", " + ex.InnerException.Message;

                    LoggingFactory.GetLogger().Log(errMsg, EventLogSeverity.Error);
                    return Json(new { success = false });
                }         
            }

            return Json(new { success = false });
        }
    }
}
