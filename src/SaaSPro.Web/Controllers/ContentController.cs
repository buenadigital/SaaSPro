using System.Web.Mvc;
using SaaSPro.Web.ViewModels;
using SaaSPro.Data.Repositories;
using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Mapping;
using SaaSPro.Infrastructure.Email;
using System.Configuration;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Controllers
{
    public class ContentController : Controller
    {
        private readonly IEmailTemplatesRepository _emailTemplatesRepository;

        public ContentController(IEmailTemplatesRepository emailTemplatesRepository)
        {
            _emailTemplatesRepository = emailTemplatesRepository;
        }

        // GET: Footer
        public ActionResult FAQs()
        {
            return View();
        }


        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                model.MailTo = ConfigurationManager.AppSettings["SendEmailTo"];
      
                var emailTemplate = _emailTemplatesRepository.GetTemplateByName(Utilities.ToDescriptionString(EmailTemplateCode.ContactRequest));
                emailTemplate = EmailTemplateFactory.ParseTemplate(emailTemplate, model);
               
                var mailMessage = MailMessageMapper.ConvertToMailMessage(emailTemplate);

                var sent = EmailServiceFactory.GetEmailService().SendMail(mailMessage);

                return PartialView("_ContactUsSuccess");
            }

            return PartialView("_ContactUs", model);
        }
    }
}