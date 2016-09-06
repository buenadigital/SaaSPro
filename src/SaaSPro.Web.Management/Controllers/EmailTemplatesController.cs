using System.Linq;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using System;
using System.Web.Mvc;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Management.Controllers
{
    public class EmailTemplatesController : ManagementControllerBase
    {
        private readonly IEmailTemplatesService _emailTemplatesService;

        public EmailTemplateListModel TemplateList
        {
            get
            {
                if (Session["EmailTemplatesList"] == null)
                {
                    return new EmailTemplateListModel();
                }
                return Session["EmailTemplatesList"] as EmailTemplateListModel;
            }
            private set { Session["EmailTemplatesList"] = value; }
        }

        public EmailTemplatesController(IEmailTemplatesService emailTemplatesService)
        {
            this._emailTemplatesService = emailTemplatesService;          
        }

        [HttpGet]
        public ActionResult Index(PagingCommand command)
        {
            TemplateList = _emailTemplatesService.List(command);

            return View(TemplateList);
        }

        public ActionResult Details(Guid id)
        {
            EmailTemplateDetailsModel model = _emailTemplatesService.Details(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Details(EmailTemplateDetailsModel model)
        {

            // removed for demo purposes
            return RedirectToAction("index")
                .AndAlert(AlertType.Warning, "Email Template updated.", "The email template was updated successfully.");

            //Guid id = _emailTemplatesService.Update(model);

            //if (id.Equals(Guid.Empty))
            //{
            //    return HttpNotFound();
            //}

            //return RedirectToAction("index")
            //    .AndAlert(AlertType.Success, "Email Template updated.", "The email template was updated successfully.");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmailTemplateDetailsModel model)
        {
                _emailTemplatesService.Add(model);
                return RedirectToAction("Index").AndAlert(AlertType.Success, "Email Template created.", "The email template was created successfully."); 
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            // removed for demo purposes
            //_emailTemplatesService.Delete(id);

            //return RedirectToAction("Index").AndAlert(AlertType.Success, "Email Template deleted.",
            //                                          "The email template was deleted successfully.");

            return RedirectToAction("index")
                .AndAlert(AlertType.Warning, "Email Template deleted.", "The email template was deleted successfully.");
        }

        public ActionResult GetTemplateContent(Guid id)
        {
            var template = new EmailTemplateListModel.EmailTemplateSummary();

            if (TemplateList.EmailTemplates != null && TemplateList.EmailTemplates.Any())
            {
                var eTemplate = TemplateList.EmailTemplates.FirstOrDefault(p => p.Id == id);
                template = eTemplate ?? template;
            }

            return PartialView("_Preview", template);
        }
    }
}
