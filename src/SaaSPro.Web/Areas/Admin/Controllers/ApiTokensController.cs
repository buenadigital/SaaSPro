using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class ApiTokensController : AdminControllerBase
    {
        private readonly IApiTokenService _apiTokenService;

        public ApiTokensController(IApiTokenService apiTokenService)
        {
            _apiTokenService = apiTokenService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ApiTokensListModel model = _apiTokenService.List(Customer.CustomerId);
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ApiTokensAddModel model)
        {
            _apiTokenService.Add(model, Customer.OriginalCustomer);

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Success.", "Api Token added successfully.");
        }

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            ApiTokensUpdateModel model = _apiTokenService.Details(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Guid id, ApiTokensUpdateModel model)
        {
            _apiTokenService.Update(model, id);

            return RedirectToAction("update", new {id = id})
                .AndAlert(AlertType.Success, "Success.", "Api Token updated successfully.");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _apiTokenService.Delete(id);
            return RedirectToAction("index")
                .AndAlert(AlertType.Warning, "Deleted.", "The Api Token was deleted successfully.");
        }
    }
}
