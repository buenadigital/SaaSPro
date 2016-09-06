using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using System;
using System.Web.Mvc;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class ReferenceListsController : AdminControllerBase
    {
        private readonly IReferenceListService _referenceListService;

        public ReferenceListsController(IReferenceListService referenceListService)
        {
            _referenceListService = referenceListService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ReferenceListsModel model = _referenceListService.ReferenceLists();         
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            ReferenceListsDetailsModel model = _referenceListService.ReferenceDetails(id, Customer.CustomerId);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddItem(Guid id, ReferenceListsAddItemModel model)
        {
            _referenceListService.AddItem(id, model, Customer.OriginalCustomer);

            return RedirectToAction("details", new {id = id})
                .AndAlert(AlertType.Success, "Reference list item added successfully.");
        }

        [HttpPost]
        public ActionResult RemoveItem(Guid id, Guid itemId)
        {
            var result = _referenceListService.RemoveItem(id, itemId);
            return Json(result);
        }
    }
}