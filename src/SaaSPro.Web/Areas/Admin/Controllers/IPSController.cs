using SaaSPro.Common.Helpers;
using SaaSPro.Common.Web;
using SaaSPro.Services.Interfaces;
using System;
using System.Web.Mvc;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Web.Areas.Admin.Controllers
{
    public class IPSController : AdminControllerBase
    {
        private readonly IIPSService _ipsService;

        public IPSController(IIPSService ipsService)
        {
            Ensure.Argument.NotNull(ipsService, "_ipsService");
            this._ipsService = ipsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _ipsService.List(Customer.CustomerId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(IPSAddModel model)
        {
            _ipsService.Add(model, Customer.OriginalCustomer);

            return RedirectToAction("index")
                .AndAlert(AlertType.Success, "Success.", "IPS Entry added successfully.");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _ipsService.Delete(id);
            return Json(true);
        }
    }
}